package com.fx.marketprice.pub;

import com.fx.marketprice.feed.Price;

/**
 * @author snehasish
 *
 * Publishes to a dummy rest endpoint
 * returns success/failure flag
 */
public interface IPublisher {

	void publish(Price msg);

}
