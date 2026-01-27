using System;

namespace JayGor.People.Api.helpers
{
    public static class HelperChat
    {
        private static string _CurrentTimeStamp="1";
        private static string _LastChangedTimeStamp = "0";
        private static bool _IsBusy;

        public static bool IsBusy
        {
            get { return _IsBusy; }
        }

        public static void SetBusy(bool state)
        {
            _IsBusy = state;
        }

        public static void SetChangedTimeStamp()
        {
            _LastChangedTimeStamp = _CurrentTimeStamp;
            _CurrentTimeStamp = Guid.NewGuid().ToString();
        }

        public static bool RequireAttention()
        {
            return !IsBusy && _CurrentTimeStamp != _LastChangedTimeStamp;
        }

        public static void SetDoneWork()
        {
            //if(!IsB4f <urgpihefiausdhfihiuhiuhlksjflñasdkjasdlñkfjasdlkñadjsfkloijñjsy)
            //{
               _LastChangedTimeStamp = _CurrentTimeStamp;
                SetBusy(false);
            //}
            //else{
              //  throw new Exception("Can not change when is busy");
            //}

        }








    }
}
