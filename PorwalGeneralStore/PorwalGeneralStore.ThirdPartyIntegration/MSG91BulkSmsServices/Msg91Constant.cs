using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices
{
    public static class Msg91Constant
    {
        public const string SEND_SINGLE_SMS_URL = "/api/sendhttp.php";
        public const string SEND_BULK_SMS_URL = "/api/v2/sendsms?country=91";
        public const string SEND_OTP_ON_EMAIL_URL = "/api/sendmailotp.php";
        public const string SEND_OTP_SMS_URL = "/api/sendotp.php";
        public const string RESEND_OTP_SMS_URL = "/api/retryotp.php";
        public const string VERIFYREQUESTOTP_OTP_SMS_URL = "/api/verifyRequestOTP.php";
    }
}
