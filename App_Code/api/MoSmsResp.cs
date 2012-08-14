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
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for MoSmsResp
/// </summary>
/// 
public enum EnumStatusCode { S1000, E1312 };

public class MoSmsResp : Message
{
  
    public String statusCode { get; set; }
    public String statusDetail { get; set; }

    private static readonly String  S1000_STATUS =  "Process completed successfully",
                                    E1312_STATUS =  "Request is Invalid. " +
                                                    "Refer the Idea Mart NBL API Developer Guide for " +
                                                    "the mandatory fields and correct format of the request.";    

    public MoSmsResp(EnumStatusCode enumStatusCode)
    {
        statusCode = (enumStatusCode.Equals(EnumStatusCode.S1000))? "S1000" : "E1312";
        statusDetail = (enumStatusCode.Equals(EnumStatusCode.S1000))? S1000_STATUS: E1312_STATUS;
    }

}