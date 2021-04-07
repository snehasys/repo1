/**
 * 
 */
package com.fx.marketprice.main;

import com.fx.marketprice.feed.MockFxPriceFeed;
import com.fx.marketprice.margin.MarginCalculator;
import com.fx.marketprice.pubsubplatform.TinyPubSubContentServer;
import com.fx.marketprice.sub.FeedSubscriber;
import com.fx.marketprice.sub.MarginSubscriber;

/**
 * @author snehasish
 *
 */
public class TestMainFlow {

	/**
	 * 
	 */
	public TestMainFlow() {
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////
		//		server code
		///////////////////////////////////////////////////////////////////////////////////////////////////////
		final String feederTopic = "Marketprice/Feed";
		final String marginGetterTopic = "Marketprice/Margin";
        TinyPubSubContentServer.getInstance().registerSubscriber(new FeedSubscriber(), feederTopic); // reads csv
        TinyPubSubContentServer.getInstance().registerSubscriber(new MarginSubscriber(), marginGetterTopic); // client price 
        // this is just an example, it is extensible to more topics
        // maybe for each ccyPair we may want to have different topics, 
        // such that, clients interested to particular ccyPair can only listen to ccyPairs they're interested
        new MockFxPriceFeed(feederTopic); 

		///////////////////////////////////////////////////////////////////////////////////////////////////////
		//        client code
		///////////////////////////////////////////////////////////////////////////////////////////////////////
        System.out.println("\n*******************************************"
    			+ "\n\tCURRENT SNAPSHOT (post margin)"
    			+ "\n*******************************************");
        System.out.println(MarginCalculator.getMarginedPrice("EUR/USD").toJson());
        System.out.println(MarginCalculator.getMarginedPrice("GBP/USD").toJson());
        System.out.println(MarginCalculator.getMarginedPrice("EUR/JPY").toJson());

	}
}
