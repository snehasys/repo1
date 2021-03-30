package com.fx.marketprice.margin;

//import java.time.LocalDateTime;

import com.fx.marketprice.feed.Price;

/**
 * @author snehasish
 *
 */
public class AlgoForMarginCalcuation {

	private static final Double bidCommission = -0.0001; // %
	private static final Double askCommission = 0.0001; // Better to put these in config files

	public AlgoForMarginCalcuation() {
	}
	
	// we may persist the prices flowing in an async db for auditing (eg. Mifid / Dodd-frank)
	public final Price calculate(final Price sourcePrice) {
		var calculatedPrice = sourcePrice;
		calculatedPrice.id += 1000000000; // to keep separate id sets for feeds and after adding commissions 
		calculatedPrice.bid += sourcePrice.bid * bidCommission;
		calculatedPrice.ask += sourcePrice.ask * askCommission;
		// calculatedPrice.ts = LocalDateTime.now(); // shall we update the timestamp of price quoted?
		
		return calculatedPrice;
	}

}
