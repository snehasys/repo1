/**
 * 
 */
package com.fx.marketprice.margin;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.locks.StampedLock;

import com.fx.marketprice.feed.Price;

/**
 * @author snehasish
 *
 */
public class MarginCalculator {
    private static ConcurrentHashMap<String, Price> ccypairPrices = new ConcurrentHashMap<>();
    private static StampedLock lock = new StampedLock();

	/**
	 * 
	 */
	public MarginCalculator() {
	}

	/**
	 * @param args
	 */
	public static void recalculateMargin(final Price price) {
		final var oldPrice = ccypairPrices.get(price.ccyPair);
        long stamp = lock.writeLock();
	    try {
	    	if (oldPrice == null 
			|| (oldPrice != null && oldPrice.ts.isBefore(price.ts))) { // this operation is not atomic, todo..
	    		System.out.println("\n The original Price received from DMA : \n" + price.toJson());
	    		ccypairPrices.put(price.ccyPair, 
	    				new AlgoForMarginCalcuation().calculate(price));
	    	}
	    } finally {
            lock.unlockWrite(stamp);
        }
	}

	public static final Price getMarginedPrice (final String ccyPair){
        var stamp = lock.readLock();
        try {
            return ccypairPrices.get(ccyPair);
        } finally {
            lock.unlock(stamp);               
        }
	}

}
