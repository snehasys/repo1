package com.global.orderbook;
/** Part A.
 *  The solution
 */

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.TreeMap;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

/**
 * @author snehasys
 *
 */
public class OrderBook {
	
	private ConcurrentMap<Long, Order> orders;   // key is orderId
	private Map<Double, List<Long>> bidOrders;   // Key is price, Value is list of orderIds which has same bid price, (NOTE: front orderId is the oldest order in the list)  
	private Map<Double, List<Long>> offerOrders; // "   "   "
	
	public OrderBook() {
		// The highest bid and lowest offer are considered "best", with all other orders stacked in price levels behind.
		this.orders      = new ConcurrentHashMap<>();
		this.bidOrders   = new TreeMap<>(Collections.reverseOrder()); // doubt: should this be concurrent too? 
		this.offerOrders = new TreeMap<>();
	}
	
	
	// (order additions are expected to occur extremely frequently)
	public void addOrder(Order o) {  
		orders.put(o.getId(), o);
		Map<Double, List<Long>> bidAskOrders = getBidAskMap(o.getSide());
		if (bidAskOrders.containsKey(o.getPrice()))  // same price quoted earlier
			bidAskOrders.get(o.getPrice()).add(o.getId());
		else
			bidAskOrders.put(o.getPrice(), new ArrayList<>(Arrays.asList(o.getId())) );
	}
	
	// (order deletions are expected to occur at approximately 60% of the rate of order additions)
	public boolean removeOrder(final long orderId)
	{
		Long id = Long.valueOf(orderId);
		if (orders.containsKey(id)) {
			orders.remove(id);
			return true;
		}
		return false;
	}
	
	//(size modifications do not effect time priority)
	public boolean modifyOrderSize(final long orderId, final long newSize)
	{
		Long id = Long.valueOf(orderId);
		if (orders.containsKey(id)) {
			orders.get(id).setSize(newSize);
			return true;
		}
		return true;
	}

	private Map<Double, List<Long>> getBidAskMap(final char side){
		return (side == 'B') ? bidOrders : offerOrders;
	}

	
//	• Given a side and a level (an integer value >0), return the price for that level (where level 1 represents the best price for a given side). 
	public double getBestPriceForLevel(final char side, final int level) {
		Optional<Double> price = getBidAskMap(side).keySet().stream().skip(level - 1).findFirst();
		if (price.isPresent())
		    return price.get();
		return -1;
	}


//	• Given a side and a level return the total size available for that level
	public long getTotalSizeForLevel(final char side, final int level) {
		Optional<List<Long>> o = getBidAskMap(side).values().stream().skip(level - 1).findFirst();
		if (!o.isPresent())  return 0;
		long[] totalSize = { 0 };
		o.get().stream().forEach(orderId -> {totalSize[0] += orders.get(orderId).getSize();});

		return totalSize[0];
	}

//	• Given a side return all the orders from that side of the book, in level- and time-order
//	NOTE: Assuming the time-order: the oldest order in same prices should be prioritized..
	public List<Order> getAllOrdersSoFar(final char side) {
		List<Order> output = new ArrayList<Order>();
		getBidAskMap(side).values().forEach(groupedOrderIds -> {
			groupedOrderIds.forEach(orderId -> {
				if (orders.containsKey(orderId))
					output.add(this.orders.get(orderId));
			});
		});
		return output;
	}
	



	/**  UNIT TESTS **
	 * TODO >> we should write a separate jUnit class for this, and add more regression
	 */
	public static void main(String[] args) {
        // NOTE: need to enable this VM Args "-enableassertions",  while compiling this file. Or all the test asserts will be ignored 
		OrderBook ob = new OrderBook();
		ob.addOrder(new Order(767,	99.99,	'O',	80));
		ob.addOrder(new Order(768,	93.94,	'B',	56));
		ob.addOrder(new Order(769,	13.99,	'B',	84));
		ob.addOrder(new Order(770,	77.5,	'O',	30));
		ob.addOrder(new Order(771,	545.9,	'B',	78));
		ob.addOrder(new Order(4767,	99.99,	'O',	81));
		ob.addOrder(new Order(4768,	193.94,	'B',	65));
		ob.addOrder(new Order(4769,	13.99,	'B',	40));
		ob.addOrder(new Order(4770,	77.5,	'O',	30));
		ob.addOrder(new Order(4771,	545.9,	'B',	78));
		ob.addOrder(new Order(5767,	99.99,	'O',	80300060));
		ob.addOrder(new Order(5768,	93.94,	'B',	9987000));
		
//		System.out.println(ob.getBestPriceForLevel('B', 2));
		assert (ob.getBestPriceForLevel('B', 2) == 193.9400);

//		System.out.println(ob.getBestPriceForLevel('B', 1));
		assert (ob.getBestPriceForLevel('B', 1) == 545.90000);
		
//		System.out.println(ob.getBestPriceForLevel('O', 1));
		assert (ob.getBestPriceForLevel('O', 1) == 77.5);

//		System.out.println(ob.getTotalSizeForLevel('O', 2));
		assert (ob.getTotalSizeForLevel('O', 2) == 80300221);
		
		assert (ob.removeOrder(770) == true);

//		System.out.println(ob.getAllOrdersSoFar('B').toString());
		assert(ob.getAllOrdersSoFar('B').toString()
				.equals( "[(771|545.9|B|78), (4771|545.9|B|78), (4768|193.94|B|65), (768|93.94|B|56), (5768|93.94|B|9987000), (769|13.99|B|84), (4769|13.99|B|40)]"));

//		System.out.println(ob.getAllOrdersSoFar('O').toString());		
		assert(ob.getAllOrdersSoFar('O').toString()
				.equals("[(4770|77.5|O|30), (767|99.99|O|80), (4767|99.99|O|81), (5767|99.99|O|80300060)]"));

		assert (ob.removeOrder(770)  == false);
		assert (ob.removeOrder(5767) == true);
		assert (ob.removeOrder(771)  == true);
		assert (ob.removeOrder(1)    == false);
		assert (ob.removeOrder(4767)  == true);
		assert (ob.removeOrder(5768) == true);
		assert (ob.removeOrder(769)  == true);
		ob.show();
	}
	
	public void show() {
		System.out.println("All->\t"   + this.orders);
		System.out.println("Bids->\t"  + this.bidOrders);
		System.out.println("Offer->\t" + this.offerOrders);
	}

} // end of class
// ==================================== ==================================== ====================================
/* Part B
	Q:
	Please suggest (but do not implement) modifications or additions to the Order and/or OrderBook classes
	to make them better suited to support real-life, latency-sensitive trading
 * 
 *  A:
    The given solution has many limitations like: 
    	1. It is relying on in memory hashMap for storing orders. 
    	   Given a very busy system with millions of incoming orders per minute, it may go out of memory pretty soon.
      2. We can have multiple instance of the service running across various datacenters to accept orders. We need better mechanisms to handle that distributed model.
    	3. Some kind of persistence mechanism should be there in the code. Like any time-series based db like KDB, or sqlServer, or MongoDB
    	4. Only the top-30 bid/ask price is enough for caching each side. Rest can be moved to a relatively slower memory, because too low bids and too high asks are not useful to anyone (because the spread will be huge)..
    	5. There may be many data-race related untested edge case bugs left in the code.
    	6. What happens to very old orders? There should be some phase out mechanism provided, that will clear up say 5hr old stale orders.
      7. Auditing the orders is missing, this may be a regulatory requirement to some regions, and not having that info may attract huge fine from the regulators (e.g. Dodd-Frank).

 
 * */

