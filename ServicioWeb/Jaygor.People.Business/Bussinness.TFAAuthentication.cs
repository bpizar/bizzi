using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        private readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();
        private readonly string AvailableKeyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public string GenerateTFASecret(int keyLength = 16)
        {
            var keyChars = new char[keyLength];
            for (int i = 0; i < keyChars.Length; i++)
            {
                keyChars[i] = AvailableKeyChars[RandomInt(AvailableKeyChars.Length)];
            }
            return new String(keyChars);
        }
        protected int RandomInt(int max)
        {
            var randomBytes = new byte[4];
            Random.GetBytes(randomBytes);
            return Math.Abs((int)BitConverter.ToUInt32(randomBytes, 0) % max);
        }

        public string GetTFACode(string secret)
        {
            return GetCodeInternal(secret, (ulong)GetInterval(DateTime.Now));
        }

        private  int IntervalSeconds;
        private long GetInterval(DateTime dateTime)
        {
            IntervalSeconds = 30;
            TimeSpan ts = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)ts.TotalSeconds / IntervalSeconds;
        }

        protected string GetCodeInternal(string secret, ulong challengeValue)
        {
            ulong chlg = challengeValue;
            byte[] challenge = new byte[8];
            for (int j = 7; j >= 0; j--)
            {
                challenge[j] = (byte)((int)chlg & 0xff);
                chlg >>= 8;
            }
            var key = Base32Encoding.ToBytes(secret);
            for (int i = secret.Length; i < key.Length; i++)
            {
                key[i] = 0;
            }
            HMACSHA1 mac = new HMACSHA1(key);
            var hash = mac.ComputeHash(challenge);
            int offset = hash[hash.Length - 1] & 0xf;
            int truncatedHash = 0;
            for (int j = 0; j < 4; j++)
            {
                truncatedHash <<= 8;
                truncatedHash |= hash[offset + j];
            }
            truncatedHash &= 0x7FFFFFFF;
            truncatedHash %= 1000000;
            string code = truncatedHash.ToString();
            return code.PadLeft(6, '0');
        }
    }
    public class Base32Encoding
    {
        public static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }
            input = input.TrimEnd('=');
            int byteCount = input.Length * 5 / 8;
            byte[] returnArray = new byte[byteCount];
            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;
            foreach (char c in input)
            {
                int cValue = CharToValue(c);
                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }
            return returnArray;
        }

        public static string ToString(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input");
            }
            int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
            char[] returnArray = new char[charCount];
            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;
            foreach (byte b in input)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }
                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
            }
            return new string(returnArray);
        }

        private static int CharToValue(char c)
        {
            int value = (int)c;
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            if (value < 123 && value > 96)
            {
                return value - 97;
            }
            throw new ArgumentException("Character is not a Base32 character.", "c");
        }

        private static char ValueToChar(byte b)
        {
            if (b < 26)
            {
                return (char)(b + 65);
            }
            if (b < 32)
            {
                return (char)(b + 24);
            }
            throw new ArgumentException("Byte is not a value Base32 value.", "b");
        }

    }
}