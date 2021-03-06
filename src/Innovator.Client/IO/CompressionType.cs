﻿namespace Innovator.Client
{
  /// <summary>
  /// Type of HTTP compression to use
  /// </summary>
  public enum CompressionType
  {
    /// <summary>
    /// No compression
    /// </summary>
    none,
    /// <summary>
    /// GZip compression
    /// </summary>
    gzip,
    /// <summary>
    /// Deflate compression
    /// </summary>
    deflate
  }
}
