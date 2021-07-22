using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private readonly byte[] _byteArray;
		public int Length => _byteArray.Length;
		private readonly int _hashcode;
		public byte this[int index] => _byteArray[index];
		public override int GetHashCode() => _hashcode;
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public ReadonlyBytes(params byte[] bytes)
		{
			_byteArray = bytes ?? throw new ArgumentNullException();
			_hashcode = GetArrayHash();
		}

		private int GetArrayHash()
		{			var primeNumber = 16777619;
			var hash = Int32.MaxValue;
			foreach (var b in _byteArray)
				unchecked
				{
					hash = (hash + b) * primeNumber;
				}
			return hash;
		}

		public IEnumerator<byte> GetEnumerator()
		{
			foreach (var b in _byteArray)
				yield return b;
		}

		public override bool Equals(object obj)
		{
			if (obj == null 
			    || this.GetType() != obj.GetType()
			    || !(obj is ReadonlyBytes otherByteArray)
			    || _byteArray.Length != otherByteArray.Length)
				return false;
			for (int i = 0; i < _byteArray.Length; i++)
				if (_byteArray[i] != otherByteArray[i])
					return false;
			return true;
		}

		public override string ToString()
		{
			var result = new StringBuilder("[");
			for (int i = 0; i < _byteArray.Length; i++)
				result.Append(string.Format($"{_byteArray[i]}, "));
			if (_byteArray.Length > 0)
				result.Remove(result.Length - 2, 2);
			result.Append("]");
			return result.ToString();
		}
	}
}