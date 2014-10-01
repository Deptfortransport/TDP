using System;
using System.Collections.Generic;
using System.Text;

namespace DailyReportFramework
{
    public enum StatusCode
    {
        Success = 0,
        UnknownStatusCode = 1,
        ErrorReadingCapacityBand = 1001,
        ErrorReadingFilePath = 1003,
        ErrorReadingTable = 1004,
        ErrorReadingSenderEmailAddress = 1005,
        ErrorReadingRecipientEmailAddress = 1006,
        ErrorReadingSmtpServer = 1007,
        ErrorSendingEmail = 1008,
        ErrorReadingFrequency = 1009,
        ErrorReadingCalendar = 1010,
        ErrorReadingFilePathDefaultInjector = 1011,
        ErrorReadingFilePathBackupInjector = 1012,
        ErrorReadingFilePathTravelineInjector = 1013,
        ErrorCopyingFile = 1024,
    }
}
