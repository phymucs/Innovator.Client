using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using Innovator.Client;

namespace System.Data.HashFunction
{
  /// <summary>
  /// Abstract implementation of an IHashFunction.
  /// Provides convenience checks and ensures a default HashSize has been set at construction.
  /// </summary>
  internal abstract class HashFunctionBase
      : IHashFunction
  {

    /// <inheritdoc />
    public int HashSize { get; }

    /// <summary>
    /// Flag to determine if a hash function needs a seekable stream in order to calculate the hash.
    /// Override to true to make <see cref="ComputeHash(Stream)" /> pass a seekable stream to <see cref="ComputeHashInternal(UnifiedData)" />.
    /// </summary>
    /// <value>
    /// <c>true</c> if a seekable stream; otherwise, <c>false</c>.
    /// </value>
    protected virtual bool RequiresSeekableStream { get { return false; } }


    /// <summary>
    /// Initializes a new instance of the <see cref="HashFunctionBase"/> class.
    /// </summary>
    /// <param name="hashSize"><inheritdoc cref="HashSize" /></param>
    protected HashFunctionBase(int hashSize)
    {
      HashSize = hashSize;
    }


    /// <inheritdoc />
    public virtual byte[] ComputeHash(byte[] data)
    {
      return ComputeHashInternal(new ArrayData(data));
    }

    /// <exception cref="System.ArgumentException">Stream \data\ must be readable.;data</exception>
    /// <inheritdoc />
    public virtual byte[] ComputeHash(Stream data)
    {
      if (!data.CanRead)
        throw new ArgumentException("Stream \"data\" must be readable.", "data");

      if (RequiresSeekableStream)
        return ComputeHashInternal(new StreamData(data.Seekable()));

      return ComputeHashInternal(new StreamData(data));
    }



    /// <summary>
    /// Computes hash value for given stream.
    /// </summary>
    /// <param name="data">Data to hash.</param>
    /// <returns>
    /// Hash value of data as byte array.
    /// </returns>
    protected abstract byte[] ComputeHashInternal(UnifiedData data);
  }
}
