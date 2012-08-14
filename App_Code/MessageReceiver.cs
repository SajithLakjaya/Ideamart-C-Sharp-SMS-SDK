/*   
   Copyright 2012 Sajith Lakjaya

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MessageReceiver
/// </summary>
public class MessageReceiver : SmsListener
{

    protected override void onMessage(MoSmsReq moSmsReq)
    {
        MtSmsResp mtSmsResp=null;
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
            
            mtSmsResp = smsSender.sendSMSReq(mtSmsReq);

        }
        catch (SdpException ex) 
        {
            Console.WriteLine(ex.StackTrace);
        }

    }
}