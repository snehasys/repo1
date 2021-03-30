package com.fx.marketprice.feed;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

// a very basic naive POJO  // avoiding getter/setters just to keep the solution small.
public class Price {
	public Integer id;
	public String ccyPair;
	public Double bid;
	public Double ask;
	public LocalDateTime ts;	

	public Price(final String s) {
		String [] data = s.split(",");

		if (data.length != 5)
			System.out.println("illegal csv line: " + s); // TODO: enrich & improve this validation logic, use async loggers
		else {
			id = Integer.parseInt(data[0]);
			ccyPair = data[1]; 					// TODO may add a ccypair validator
			bid = Double.parseDouble(data[2]);
			ask = Double.parseDouble(data[3]);
			ts = LocalDateTime.parse(data[4], DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm:ss:SSS"));
		}
	}
	
	public String toJson() {      // Better if we are using well tested libs like jackson/Gson etc
		var sb = new StringBuilder(" {");
		sb.append("Id:"); sb.append(id);
		sb.append(",\tCcyPair:"); sb.append(ccyPair);
		sb.append(",\tBid:"); sb.append(bid);
		sb.append(",\tAsk:"); sb.append(ask);
		sb.append(",\tTimeStamp:"); sb.append(ts);
		sb.append("} ");

		return sb.toString();
	}

}
