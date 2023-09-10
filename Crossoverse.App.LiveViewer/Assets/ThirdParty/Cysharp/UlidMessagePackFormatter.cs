//
// The original source code is available on GitHub.
// https://github.com/Cysharp/Ulid/blob/1.3.0/src/Ulid.MessagePack/UlidMessagePackFormatter.cs
//
// -----
//
// MIT License
//
// Copyright (c) 2019 Cysharp, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// -----

using MessagePack;
using System.Buffers;
using MessagePack.Formatters;
using System;

namespace Cysharp.Serialization.MessagePack
{
    public class UlidMessagePackFormatter : IMessagePackFormatter<Ulid>
    {
        public Ulid Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var bin = reader.ReadBytes();
            if (bin == null)
            {
                throw new MessagePackSerializationException(string.Format("Unexpected msgpack code {0} ({1}) encountered.", MessagePackCode.Nil, MessagePackCode.ToFormatName(MessagePackCode.Nil)));
            }

            var seq = bin.Value;
            if (seq.IsSingleSegment)
            {
                return new Ulid(seq.First.Span);
            }
            else
            {
                Span<byte> buf = stackalloc byte[16];
                seq.CopyTo(buf);
                return new Ulid(buf);
            }
        }

        public void Serialize(ref MessagePackWriter writer, Ulid value, MessagePackSerializerOptions options)
        {
            const int Length = 16;

            writer.WriteBinHeader(Length);
            var buffer = writer.GetSpan(Length);
            value.TryWriteBytes(buffer);
            writer.Advance(Length);
        }
    }

    public class UlidMessagePackResolver : IFormatterResolver
    {
        public static IFormatterResolver Instance = new UlidMessagePackResolver();

        UlidMessagePackResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return Cache<T>.formatter;
        }

        static class Cache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static Cache()
            {
                if (typeof(T) == typeof(Ulid))
                {
                    formatter = (IMessagePackFormatter<T>)(object)new UlidMessagePackFormatter();
                }
            }
        }
    }
}