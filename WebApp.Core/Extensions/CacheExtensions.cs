using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed {
    internal static class CacheExtensions {
        public static T Get<T> (this IDistributedCache cache, string key) {
            var bytes = cache.Get (key);
            if (bytes == null || bytes.Length == 0) {
                return default (T);
            }

            using (var memoryStream = new MemoryStream (bytes)) {
                var binaryFormatter = new BinaryFormatter ();
                return (T) binaryFormatter.Deserialize (memoryStream);
            }
        }

        public static async Task<T> GetAsync<T> (this IDistributedCache cache, string key, CancellationToken cancellationToken = default (CancellationToken)) {
            byte[] bytes = await cache.GetAsync (key, cancellationToken);
            if (bytes == null || bytes.Length == 0) {
                return default (T);
            }

            using (MemoryStream memoryStream = new MemoryStream (bytes)) {
                var binaryFormatter = new BinaryFormatter ();
                return (T) binaryFormatter.Deserialize (memoryStream);
            }
        }

        public static Task SetAsync<T> (this IDistributedCache cache, string key, T value, CancellationToken cancellationToken = default (CancellationToken)) {
            byte[] bytes;
            using (var memoryStream = new MemoryStream ()) {
                var binaryFormatter = new BinaryFormatter ();
                binaryFormatter.Serialize (memoryStream, value);
                bytes = memoryStream.ToArray ();
            }

            return cache.SetAsync (key, bytes, cancellationToken);
        }

        public static void Set<T> (this IDistributedCache cache, string key, T value) {
            byte[] bytes;
            using (var memoryStream = new MemoryStream ()) {
                var binaryFormatter = new BinaryFormatter ();
                binaryFormatter.Serialize (memoryStream, value);
                bytes = memoryStream.ToArray ();
            }

            cache.Set (key, bytes);
        }
    }
}