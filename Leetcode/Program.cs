using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode
{
    class Program
    {
        #region Sol1
        public class Solution1
        {
            public int[] TwoSum(int[] nums, int target)
            {
                Dictionary<int, int> dict = new Dictionary<int, int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    int complement = target - nums[i];

                    if (dict.ContainsKey(complement))
                        return new [] { dict[complement], i };
                    if (!dict.ContainsKey(nums[i]))
                        dict.Add(nums[i], i);
                }
                throw new Exception("Not Found.");

                /*
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[i] + nums[j] == target)
                        {
                            return new[] { i, j };
                        }
                        else
                            continue;
                    }
                }
                throw new Exception("Not Found.");
                */
            }
        }
        #endregion

        #region Sol2
        public class Solution2
        {
            public class ListNode
            {
                public int val;
                public ListNode next;
                public ListNode(int x) { val = x; }
            }

            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                ListNode dummy = new ListNode(0);
                ListNode p = l1, q = l2, cur = dummy;
                int carry = 0;
                while(p != null || q != null)
                {
                    int x = p != null ? p.val : 0;
                    int y = q != null ? q.val : 0;
                    int sum = carry + x + y;
                    carry = sum / 10;
                    cur.next = new ListNode(sum % 10);
                    cur = cur.next;
                    if (p != null) p = p.next;
                    if (q != null) q = q.next;
                }
                if (carry > 0)
                    cur.next = new ListNode(1);
                return dummy.next;
            }

            //public ListNode Sub(ListNode l1, ListNode l2, bool carry)
            //{
            //    if (l1 == null && l2 == null)
            //    {
            //        return carry ? new ListNode(1) : null;
            //    }
            //    else
            //    {
            //        var n = (l1?.val ?? 0) + (l2?.val ?? 0) + (carry ? 1 : 0);
            //        var new_carry = n >= 10;
            //        var new_n = new_carry ? n - 10 : n;

            //        return new ListNode(new_n)
            //        {
            //            next = Sub(l1?.next, l2?.next, new_carry)
            //        };
            //    }
            //}
            //public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            //{
            //    return Sub(l1, l2, false);
            //}
        }
        #endregion

        #region Sol3
        public class Solution3
        {
            public int[] lengthToSame(char[] cs)
            {
                int[] lengthToSameCh = new int[cs.Length];

                for (int i = 0; i < cs.Length; i++)
                {
                    lengthToSameCh[i] = 1;

                    for (int j = i + 1; j < cs.Length; j++)
                    {
                        if (cs[i] == cs[j])
                        {
                            lengthToSameCh[i] = j - i;
                            break;
                        }
                        else if (j == cs.Length - 1)
                        {
                            lengthToSameCh[i] = j - i + 1;
                        }
                    }

                }
                return lengthToSameCh;
            }

            public int[] dp(int[] los)
            {
                int[] ret = new int[los.Length];
                for (int i = los.Length - 1; i >= 0; i--)
                {
                    if (i == los.Length - 1)
                    {
                        ret[i] = los[i];
                    }
                    else
                    {
                        ret[i] = Math.Min(ret[i + 1] + 1, los[i]);
                    }
                }
                return ret;
            }

            public int LengthOfLongestSubstring(string s)
            {
                if (string.IsNullOrEmpty(s)) return 0;
                char[] cs = s.ToCharArray();
                int[] los = lengthToSame(cs);
                int[] ret = dp(los);
                return ret.Max();
            }

            //public int LengthOfLongestSubstring2(string s)
            //{
            //    int n = s.Length, ans = 0;
            //    System.Collections.Hashtable ht = new System.Collections.Hashtable();

            //    for (int i = 0, j = 0; j < n; j++)
            //    {
            //        if (ht.ContainsKey(s.ElementAt(j)))
            //        {
            //            i = Math.Max(i, (int)ht[s.ElementAt(j)]);
            //        }
            //        ans = Math.Max(ans, j - i + 1);
            //        ht[s.ElementAt(j)] = j + 1;
            //    }
            //    return ans;
            //}

            //public int LengthOfLongestSubstring(string s)
            //{
            //    int n = s.Length, ans = 0;
            //    Dictionary<char, int> dict = new Dictionary<char, int>();

            //    for (int i = 0, j = 0; j < n; j++)
            //    {
            //        if (dict.ContainsKey(s.ElementAt(j)))
            //        {
            //            i = Math.Max(i, dict[s.ElementAt(j)]);
            //            dict.Remove(s.ElementAt(j));
            //        }
            //        ans = Math.Max(ans, j - i + 1);
            //        dict.Add(s.ElementAt(j), j + 1);
            //    }
            //    return ans;
            //}
        }
        #endregion

        #region Q4
        public class Solution4
        {
            public int[] sort(int[] x, int[] y, int n)
            {
                int[] ret = new int[n];

                for (int i = 0, j = 0, k = 0; k < n; k = i + j)
                {
                    if (i >= x.Length)
                    {
                        ret[k] = y[j++];
                        continue;
                    }
                    if (j >= y.Length)
                    {
                        ret[k] = x[i++];
                        continue;
                    }
                    if (x[i] <= y[j])
                        ret[k] = x[i++];
                    else
                        ret[k] = y[j++];
                }

                return ret;
            }
            public double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                int len = nums1.Length + nums2.Length;

                int[] sorted = sort(nums1, nums2, len / 2 + 1);

                if (len % 2 == 0)
                    return (sorted[(len - 1) / 2] + sorted[len / 2]) / 2.0;
                else
                    return sorted[(len - 1) / 2] + 0.0;
            }
        }
        #endregion

        #region Q5
        public class Solution5
        {
            public string LongestPalindrome(string s)
            {
                var len = s.Length;
                if (len == 1 || string.IsNullOrEmpty(s)) return s;
                int[] p = new int[2 * len + 1];
                for (int i = 1; i < 2 * len; i++)
                {
                    if (i % 2 != 0)
                    {
                        var pivot = (i - 1) / 2;
                        for (int j = 1; pivot - j > -1 && pivot + j < len; j++)
                        {
                            if (s[pivot - j] == s[pivot + j]) p[i] = 2 * j + 1;
                            else break;
                        }
                    }
                    else
                    {
                        var R = i / 2;
                        var L = R - 1;
                        for (int j = 0; L - j > -1 && R + j < len; j++)
                        {
                            if (s[L - j] == s[R + j]) p[i] = 2 * j + 2;
                            else break;
                        }
                    }
                }
                int maxValue = p.Max();
                // return string.Join("", p);
                int maxIndex = p.ToList().IndexOf(maxValue);
                return s.Substring((maxIndex - maxValue) / 2, Math.Max(1, maxValue));
            }

            //public string sub(string s, int[] rad)
            //{
            //    int max_idx = 0, max_len = 0;
            //    for (int i = 0; i < rad.Length; i++)
            //    {
            //        if (rad[i] > max_len)
            //        {
            //            max_idx = i;
            //            max_len = rad[i];
            //        }
            //    }
            //    if (max_idx % 2 == 0)
            //        return s.Substring(max_idx / 2 - (max_len - 1) / 2, max_len);
            //    else
            //        return s.Substring((max_idx - 1) / 2 - max_len / 2 + 1, max_len);
            //}
            //public string LongestPalindrome(string s)
            //{
            //    int len = s.Length;
            //    int[] rad = new int[2 * len];
            //    for (int i = 0, j = 0, k = 0; i < 2 * len; i += k, j = Math.Max(j - k, 0))
            //    {
            //        while (i - j >= 0 && i + j + 1 < 2 * len && s.ElementAt((i - j) / 2) == s.ElementAt((i + j + 1) / 2))
            //            ++j;
            //        rad[i] = j;
            //        for (k = 1; i - k >= 0 && rad[i] - k >= 0 && rad[i - k] != rad[i] - k; ++k)
            //            rad[i + k] = Math.Min(rad[i - k], rad[i] - k);
            //    }
            //    return sub(s, rad); // ret. centre of the longest palindrom
            //}


            //public string LongestPalindrome(string s)
            //{
            //    bool[,] map = new bool[s.Length, s.Length];
            //    int ans_len = 0;
            //    int ans_x = 0;

            //    for (int len = 1; len <= s.Length; len++)
            //    {
            //        for (int i = 0, j = len - i - 1; j < s.Length; i++, j++)
            //        {
            //            // Debug.WriteLine($"{(i,j)}: {s.Substring(i, len)}");
            //            if (i == j)
            //                map[i, j] = true;
            //            else if (i + 1 == j)
            //                map[i, j] = s.ElementAt(i) == s.ElementAt(j);
            //            else
            //                map[i, j] = map[i + 1, j - 1] && s.ElementAt(i) == s.ElementAt(j);

            //            if (map[i, j] && ans_len < j - i + 1)
            //            {
            //                ans_len = j - i + 1;
            //                ans_x = i;
            //            }
            //        }
            //    }

            //    return s.Substring(ans_x, ans_len);
            //}

            //Dictionary<(int, int), bool> map = new Dictionary<(int, int), bool>();
            //public bool dp(string s, int i, int j)
            //{
            //    if (!map.ContainsKey((i, j)))
            //    {
            //        if (i == j)
            //            map.Add((i, j), true);
            //        else if (i + 1 == j)
            //            map.Add((i, j), s.ElementAt(i) == s.ElementAt(j));
            //        else
            //            map.Add((i, j), dp(s, i + 1, j - 1) && s.ElementAt(i) == s.ElementAt(j));
            //    }
            //    return map[(i, j)];
            //}
            //public string LongestPalindrome(string s)
            //{
            //    for (int len = s.Length; len >= 0; len--)
            //    {
            //        for (int i = 0, j = len - 1; j < s.Length; i++, j++)
            //        {
            //            if (dp(s, i, j))
            //                return s.Substring(i, j - i + 1);
            //        }
            //    }

            //    return s.Substring(0, 1);
            //}

            //public string LongestPalindrome(string s)
            //{
            //    if (string.IsNullOrEmpty(s)) return "";
            //    int start = 0, end = 0;
            //    for (int i = 0; i < s.Length; i++)
            //    {
            //        int len1 = expandAround(s, i, i);
            //        int len2 = expandAround(s, i, i + 1);
            //        int len = Math.Max(len1, len2);
            //        if (len > end - start)
            //        {
            //            start = i - (len - 1) / 2;
            //            end = i + len / 2;
            //        }
            //    }
            //    return s.Substring(start, end - start + 1);
            //}

            //public int expandAround(string s, int i, int j)
            //{
            //    int L = i, R = j;
            //    while(L>=0 && R < s.Length && s.ElementAt(L) == s.ElementAt(R))
            //    {
            //        L--;
            //        R++;
            //    }
            //    return R - L - 1;
            //}
        }
        #endregion

        static void Main(string[] args)
        {
            #region Q5
            string q51 = new Solution5().LongestPalindrome("babad");
            Console.WriteLine(q51);
            string q52 = new Solution5().LongestPalindrome("cbbd");
            Console.WriteLine(q52);
            string q53 = new Solution5().LongestPalindrome("ccd");
            Console.WriteLine(q53);
            string q54 = new Solution5().LongestPalindrome("aacdefcaa");
            Console.WriteLine(q54);
            string q55 = new Solution5().LongestPalindrome("a");
            Console.WriteLine(q55);
            string q56 = new Solution5().LongestPalindrome("aa");
            Console.WriteLine(q56);
            string q57 = new Solution5().LongestPalindrome("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Console.WriteLine(q57);
            #endregion

            #region Q4
            var q41 = new Solution4().FindMedianSortedArrays(new[] { 1, 3 }, new[] { 2 });
            Console.WriteLine("OK");
            var q42 = new Solution4().FindMedianSortedArrays(new[] { 1, 3 }, new[] { 2, 4 });
            Console.WriteLine("OK");
            #endregion

            #region Q3
            var q30 = new Solution3().LengthOfLongestSubstring("abcabcbb");
            Console.WriteLine(q30);
            var q31 = new Solution3().LengthOfLongestSubstring("bbbbb");
            Console.WriteLine(q31);
            var q32 = new Solution3().LengthOfLongestSubstring("pwwkew");
            Console.WriteLine(q32);
            var q33 = new Solution3().LengthOfLongestSubstring("jlygy");
            Console.WriteLine(q33);
            #endregion

            #region Q2
            var x = new Solution2.ListNode(2)
            {
                next = new Solution2.ListNode(4)
                {
                    next = new Solution2.ListNode(3)
                }
            };

            var y = new Solution2.ListNode(5)
            {
                next = new Solution2.ListNode(6)
                {
                    next = new Solution2.ListNode(4)
                }
            };

            var x1 = new Solution2.ListNode(9);
            var y1 = new Solution2.ListNode(1)
            {
                next = new Solution2.ListNode(9)
                {
                    next = new Solution2.ListNode(9)
                }
            };

            var q2 = new Solution2().AddTwoNumbers(x1, y1);
            Console.Write("ok");
            #endregion

            #region Q1
            var q1 = new Solution1().TwoSum(new[] { 5, 75, 25 }, 100);
            Console.WriteLine(q1);
            #endregion

        }
    }
}
