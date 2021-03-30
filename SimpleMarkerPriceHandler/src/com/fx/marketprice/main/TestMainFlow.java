/**
 * 
 */
package com.fx.marketprice.main;

import com.fx.marketprice.feed.MockFxPriceFeed;
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
		// TODO Auto-generated constructor stub
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		final String feederTopic = "Marketprice/Feed";
		final String marginGetterTopic = "Marketprice/Margin";
        TinyPubSubContentServer.getInstance().registerSubscriber(new FeedSubscriber(), feederTopic);
        TinyPubSubContentServer.getInstance().registerSubscriber(new MarginSubscriber(), marginGetterTopic);
        // this is just a example, it is extensible to more topics
        // maybe for each ccyPiar we may want to have different topics, 
        // such that, clients interested to particular ccyPair can only listen to ccyPairs they're interested

        new MockFxPriceFeed(feederTopic);		

	}

}
