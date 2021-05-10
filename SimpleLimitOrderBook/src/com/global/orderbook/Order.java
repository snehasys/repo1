/**
 * 
 */
package com.global.orderbook;

/**
 * @author snehasys
 *
 */
public class Order {
	
	private long id; // id of order
	private double price;
	private char side; // B->Bid, O->Offer
	private long size;
	
	/**
	 * @param id
	 * @param price
	 * @param side
	 * @param size
	 */
	public Order(long id, double price, char side, long size) {
		this.id = id;
		this.price = price;
		this.side = side;
		this.size = size;
	}
	public long getId() {
		return id;
	}
	public double getPrice() {
		return price;
	}
	public char getSide() {
		return side;
	}
	public long getSize() {
		return size;
	}
	
	public void setSize (final long size) {
		this.size = size;
	}
	
	@Override
	public String toString() {
		StringBuilder sb = new StringBuilder();
		sb.append("(");
		sb.append(id).append("|");
		sb.append(price).append("|");
		sb.append(side).append("|");
		sb.append(size);
		sb.append(")");
		
		return sb.toString();
	}
	
}


