using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MedianaFilterService
{
    public class TargetArray
    {
        public int[] array;
        private int arraySize;
        public void setArrayFromString(string arrayAsString)
        {
            string[] splittedArray = arrayAsString.Split(',');
            arraySize = splittedArray.Length;
            array = new int[splittedArray.Length];
            for (int i = 0; i < arraySize; i++ )
            {
                Int32.TryParse(splittedArray[i], out array[i]);
    //            array[i]++;
            }
        }
        public string getArrayAsString()
        {
            string result = "";
            for (int i = 0; i < arraySize; i++) result += (i==0? "":",") + array[i];
            return result;
        }

        private void fillWindowArray(int pos, int window, ref int[] windowArray)
        {
            int leftShift = - window / 2;
            for (int i = 0; i < window; i++ )
            {
                int currentPos = i + pos - window / 2;
                if (currentPos < 0) currentPos = 0;
                else if (currentPos >= arraySize) currentPos = arraySize - 1;
                windowArray[i] = array[currentPos];
            }
        }

        public void filterArray(int window)
        {
            int[] newArray = new int[arraySize];
            int[] windowArray = new int[window];
            for (int i = 0; i < arraySize; i++ )
            {
                fillWindowArray(i, window, ref windowArray);
                Array.Sort(windowArray);
                newArray[i] = windowArray[window / 2];
            }
            array = newArray;
        }
    }
}
