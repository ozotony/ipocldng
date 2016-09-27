using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ipong.InterSwitch.PayDirect.Classes
{
    public class ErrorHandler
    {
        public string getErrorDesc(string code)
        {
            string desc = "";
            switch (code)
            {
                case "00": desc = "Approved by Financial Institution"; break;
                case "01": desc = "Refer to Financial Institution"; break;
                case "02": desc = "Refer to Financial Institution, Special Condition"; break;
                case "03": desc = "Invalid Merchant"; break;
                case "04": desc = "Pick-up card"; break;
                case "05": desc = "Do Not Honor"; break;
                case "06": desc = "Error"; break;
                case "07": desc = "Pick-Up Card, Special Condition"; break;
                case "08": desc = "Honor with Identification"; break;
                case "09": desc = "Request in Progress"; break;

                case "10": desc = "Approved by Financial Institution, Partial"; break;
                case "11": desc = "Approved by Financial Institution, VIP"; break;
                case "12": desc = "Invalid Transaction"; break;
                case "13": desc = "Invalid Amount"; break;
                case "14": desc = "Invalid Card Number"; break;
                case "15": desc = "No Such Financial Institution"; break;
                case "16": desc = "Approved by Financial Institution, Update Track 3"; break;
                case "17": desc = "Customer Cancellation"; break;
                case "18": desc = "Customer Dispute"; break;
                case "19": desc = "Re-enter Transaction"; break;

                case "20": desc = "Invalid Response from Financial Institution"; break;
                case "21": desc = "No Action Taken by Financial Institution"; break;
                case "22": desc = "Suspected Malfunction"; break;
                case "23": desc = "Unacceptable Transaction Fee"; break;
                case "24": desc = "File Update not Supported"; break;
                case "25": desc = "Unable to Locate Record"; break;
                case "26": desc = "Duplicate Record"; break;
                case "27": desc = "File Update Field Edit Error"; break;
                case "28": desc = "File Update File Locked"; break;
                case "29": desc = "File Update Failed"; break;

                case "30": desc = "Format Error"; break;
                case "31": desc = "Bank Not Supported"; break;
                case "32": desc = "Completed Partially by Financial Institution"; break;
                case "33": desc = "Expired Card, Pick-Up"; break;
                case "34": desc = "Suspected Fraud, Pick-Up"; break;
                case "35": desc = "Contact Acquirer, Pick-Up"; break;
                case "36": desc = "Restricted Card, Pick-Up"; break;
                case "37": desc = "Call Acquirer Security, Pick-Up"; break;
                case "38": desc = "PIN Tries Exceeded, Pick-Up"; break;
                case "39": desc = "No Credit Account"; break;

                case "40": desc = "Function not Supported"; break;
                case "41": desc = "Lost Card, Pick-Up"; break;
                case "42": desc = "No Universal Account"; break;
                case "43": desc = "Stolen Card, Pick-Up"; break;
                case "44": desc = "No Investment Account"; break;

                case "51": desc = "Insufficient Funds"; break;
                case "52": desc = "No Check Account"; break;
                case "53": desc = "No Savings Account"; break;
                case "54": desc = "Expired Card"; break;
                case "55": desc = "Transaction Error"; break;
                case "56": desc = "No Card Record"; break;
                case "57": desc = "Transaction not permitted to Cardholder"; break;
                case "58": desc = "Transaction not permitted on Terminal"; break;
                case "59": desc = "Suspected Fraud"; break;

                case "60": desc = "Contact Acquirer"; break;
                case "61": desc = "Exceeds Withdrawal Limit"; break;
                case "62": desc = "Restricted Card"; break;
                case "63": desc = "Security Violation"; break;
                case "64": desc = "Original Amount Incorrect"; break;
                case "65": desc = "Exceeds withdrawal frequency"; break;
                case "66": desc = "Call Acquirer Security"; break;
                case "67": desc = "Hard Capture"; break;
                case "68": desc = "Response Received Too Late"; break;

                case "75": desc = "PIN tries exceeded"; break;
                case "76": desc = "Reserved for Future Postilion Use"; break;
                case "77": desc = "Intervene, Bank Approval Required"; break;
                case "78": desc = "Intervene, Bank Approval Required for Partial Amount"; break;

                case "90": desc = "Cut-off in Progress"; break;
                case "91": desc = "Issuer or Switch Inoperative"; break;
                case "92": desc = "Routing Error"; break;
                case "93": desc = "Violation of law"; break;
                case "94": desc = "Duplicate Transaction"; break;
                case "95": desc = "Reconcile Error"; break;
                case "96": desc = "System Malfunction"; break;
                case "98": desc = "Exceeds Cash Limit"; break;

                case "A0": desc = "Unexpected error"; break;
                case "A4": desc = "Transaction not permitted to card holder, via channels"; break;
                case "Z0": desc = "Transaction Status Unconfirmed"; break;
                case "Z1": desc = "Transaction Error"; break;
                case "Z2": desc = "Bank account error"; break;
                case "Z3": desc = "Bank collections account error"; break;
                case "Z4": desc = "Interface Integration Error"; break;
                case "Z5": desc = "Duplicate Reference Error"; break;
                case "Z6": desc = "Incomplete Transaction"; break;
                case "Z7": desc = "Transaction Split Pre-processing Error"; break;
                case "Z8": desc = "Invalid Card Number, via channels"; break;
                case "Z9": desc = "Transaction not permitted to card holder, via channels"; break;
                case "20031": desc = "Invalid value for ProductId"; break; 

                default: desc = "NA"; break;
            }

            return desc;
        }

       // public string getErrorCodes
    }
}