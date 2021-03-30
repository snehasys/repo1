package com.fx.marketprice.pubsubplatform;

import java.util.Arrays;
import java.util.List;
import java.util.concurrent.ConcurrentHashMap;

import com.fx.marketprice.sub.ISubscriber;

/*
 * a singleton class to manage pubsub topics*/
public class TinyPubSubContentServer {
	//                      TopicName   subscribers
	private ConcurrentHashMap<String, List<ISubscriber>> subscriberLists;

    private static TinyPubSubContentServer serverInstance;

    public static TinyPubSubContentServer getInstance() {
        if (serverInstance == null) {
            serverInstance = new TinyPubSubContentServer();
        }
        return serverInstance;
    }

    private TinyPubSubContentServer() {
    	// TODO Auto-generated constructor stub
        this.subscriberLists = new ConcurrentHashMap<>();
    }
    
    public void sendMessage(String topic, String msg) {
        List<ISubscriber> subs = subscriberLists.get(topic);
        for (ISubscriber s : subs) {
            s.onMessageUpdateCallback(topic, msg);
        }
    }

    public void registerSubscriber(ISubscriber subscriber, String topic) { // Q: is it thread safe?
    	if (!subscriberLists.containsKey(topic))
    		subscriberLists.put(topic, Arrays.asList(subscriber));
    	else
    		subscriberLists.get(topic).add(subscriber); // a topic can have more than one subscribers
    }
}
