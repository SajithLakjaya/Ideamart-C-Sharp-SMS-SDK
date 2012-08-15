Ideamart C# SMS Apis
=====================

Required
---------
* Microsoft .NET Framework 4
* Ideamart SMS Simulator
  You can download Ideamart Multiuser SMS Simulator using following link
		https://github.com/SajithLakjaya/Ideamart-Multiuser-SMS-Simulator/downloads

How to create an SMS Application
--------------------------------
* First you should add class into your project which placed api folder
* Apis provides two classes to receive and send messages

  * To receive messages : SmsListener
  * To send messages : SmsSender

Receiving SMS
--------------
* To receive SMS, you should create a class and extends SmsListener
* Then override onMessage abstract method on your class

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Web;

        public class MessageReceiver : SmsListener
        {
	        protected override void onMessage(MoSmsReq moSmsReq)
	        {
		        Console.WriteLine(moSmsReq.getMessage());
	        }
        }

* SMS receiving application is a simple web application. Therefore you must configure web.xml as below

        <?xml version="1.0"?>
          <configuration>
	          <system.web>
		          <httpHandlers>
		        	  <add verb="*" path="*" type="MessageReceiver"/>
	      	    </httpHandlers>
              <compilation debug="true" targetFramework="4.0"/>
	          </system.web>
          </configuration>

* When the application is started, if a SMS received to the application onMessage method in your class will be executed. You can get sms details which is received to the application using moSmsReq object inside onMessage method

Sending SMS
------------
* For sending SMS, you should create an instance of SmsSender as below
* SendSMSReq method in SmsSender class throw SdpException. Therefore you should catch that exception on your code

        try
        {
            SmsSender smsSender = new SmsSender("http://127.0.0.1:7007/service");

            List<String> address = new List<String>();
            address.Add(moSmsReq.sourceAddress);

            MtSmsReq mtSmsReq = new MtSmsReq();
            mtSmsReq.applicationId = moSmsReq.applicationID;
            mtSmsReq.password = "password";
            mtSmsReq.destinationAddresses = address;

            mtSmsReq.message = "Message Received. Thanks you.";
            
            MtSmsResp mtSmsResp = smsSender.sendSMSReq(mtSmsReq);

        }
        catch (SdpException ex) 
        {
            Console.WriteLine(ex.StackTrace);
        }

More info
-------------
* For more info tweet @sajithlakjaya


	