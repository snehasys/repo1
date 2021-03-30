
package com.fx.marketprice.sub;
import java.lang.String;

/**
 * @author snehasish
 *
 */
public interface ISubscriber {
	void onMessageUpdateCallback (final String topic, final String msg);

}
