#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("NYcEJzUIAwwvg02D8ggEBAQABQaMhK2xxgUR+9JirElkA6ofIcAo1GYKVXS0R9I1B8hQnt+Nicbw1m5Uvp3wGpP7hemczAiukHGrn0pQggICeCCcot0LGI98ebbMXYoHYmuvm0WEB/8cWgCpxldSUBo75zN6jBRplZ0/WxnW1KQKS5qX9ZBmweIO0l1VOTYQn3HgTOLpt85XNOwowkRqAMhl+eBwygFWJrfwBTOUsAGh5bG7hwQKBTWHBA8HhwQEBYvuBBIpESQn3qCx0nezaI1lV4T/T+uGqoJ71TxMAy4D9ADrlV94OtKLZmIvOAT9Un7rg8E5G6I5dkl5piY+v3hhsHx4B10hGmKmKNAuLoS4bzinwvx8vMZakx6Srl5OuAcGBAUE");
        private static int[] order = new int[] { 0,8,7,9,9,5,7,10,13,13,13,11,13,13,14 };
        private static int key = 5;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
