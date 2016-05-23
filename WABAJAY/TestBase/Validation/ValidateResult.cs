using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBase.Validation
{
    public static class ValidateResult
    {
        //public static bool CheckAttributesInList(List<string> sourceList, List<string> destinationList)
        //{
        //    //foreach (string itemSource in sourceList)
        //    //{
        //    //    foreach (string itemDestination in destinationList)
        //    //    {
        //    for (int i = 0; i < sourceList.Count; i++)
        //    {
        //        if (!(string.Equals(sourceList[i], destinationList[i])))
        //        {
        //            return false;
        //        }
        //    }
           
        //        //}   
        //    //}

        //    return true;
        //}
        public static bool CheckAttributesInNameValueCollection(NameValueCollection sourceList, NameValueCollection destinationList)
        {
            //foreach (string itemSource in sourceList)
            //{
            //    foreach (string itemDestination in destinationList)
            //    {
            for (int i = 0; i < sourceList.Count; i++)
            {
                if (!(string.Equals(sourceList[i], destinationList[i])))
                {
                    return false;
                }
            }

            //}   
            //}

            return true;
        }
        public static bool CheckAttributesInBooleanList(List<bool> sourceList, List<bool> destinationList)
        {
            //foreach (string itemSource in sourceList)
            //{
            //    foreach (string itemDestination in destinationList)
            //    {
            for (int i = 0; i < sourceList.Count; i++)
            {
                if (!(string.Equals(sourceList[i], destinationList[i])))
                {
                    return false;
                }
            }

            //}   
            //}

            return true;
        }
        public static bool CheckAttributesInList(List<string> attributesActual, List<string> attributesExpected)
        {
            if (attributesActual.Count == attributesExpected.Count)
            {
                for (int i = 0; i < attributesActual.Count; i++)
                {
                    if (!(string.Equals(attributesActual[i], attributesExpected[i])))
                        return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool AssertForAttributesWidget(List<string> attributesActual, List<string> attributesExpected)
        {
            int flag = 0;
            for (int i = 0; i < attributesActual.Count; i++)
            {
                for (int j = 0; j < attributesExpected.Count; j++)
                {
                    if (!(attributesActual[i].TrimEnd() == attributesExpected[j].TrimEnd()))
                    {
                        continue;
                    }

                    else
                    {
                        flag++;
                    }
                }
            }

            if (flag == attributesActual.Count)
            {
                return true;
            }

            return false;
        }
        //public static bool AssertForAttributesWidgetNameValueCollection(NameValueCollection attributesActual, NameValueCollection attributesExpected)
        //{
        //    int flag = 0;
        //    for (int i = 0; i < attributesActual.Count; i++)
        //    {
        //        for (int j = 0; j < attributesExpected.Count; j++)
        //        {
        //            if (!(attributesActual[i].TrimEnd() == attributesExpected[j].TrimEnd()))
        //            {
        //                continue;
        //            }

        //            else
        //            {
        //                flag++;
        //            }
        //        }
        //    }

        //    if (flag == attributesActual.Count)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

    }
}
