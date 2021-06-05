using EeveeTools.Database;

namespace _13B_REW.Bancho {
    public static class GlobalVariables {
        public static DatabaseContext DatabaseContext = new("postgres", "ssh", "127.0.0.1", "rx7");
    }
}
