﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace nkxtract
{
  /// <summary>
  /// A stream based on another stream, useful for representing
  /// file streams within simple archive packages.
  /// </summary>
  public class OffsetStream : Stream
  {
    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite => false;

    public override long Length { get; }

    private Stream pkg;
    private long data_offset;

    private long _position;
    public override long Position
    {
      get
      {
        return _position;
      }

      set
      {
        Seek(value, SeekOrigin.Begin);
      }
    }

    /// <summary>
    /// Constructs a new offset stream on the given base stream with the given offset and length.
    /// </summary>
    /// <param name="package">The base stream</param>
    /// <param name="offset">Offset into the base stream where this stream starts</param>
    /// <param name="length">Number of bytes in this stream</param>
    public OffsetStream(Stream package, long offset, long length)
    {
      this.pkg = package;
      this.data_offset = offset;
      Length = length;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      pkg.Seek(data_offset + Position, SeekOrigin.Begin);
      if (count + Position > Length)
      {
        count = (int)(Length - Position);
      }
      int bytes_read = pkg.Read(buffer, offset, count);
      _position += bytes_read;
      return bytes_read;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          break;
        case SeekOrigin.Current:
          offset += _position;
          break;
        case SeekOrigin.End:
          offset += Length;
          break;
      }
      if (offset > Length)
      {
        offset = Length;
      }
      else if (offset < 0)
      {
        offset = 0;
      }
      _position = offset;
      return _position;
    }

    #region Not Supported
    public override void Flush()
    {
      throw new NotSupportedException();
    }
    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }
    #endregion
  }
}