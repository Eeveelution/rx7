using System.IO;
using RX7.Bancho;
using RX7.Bancho.Packets;

MemoryStream stream = new();
using EeveeTools.Helpers.BanchoWriter writer = new(stream);

writer.Write(123);

writer.Write((byte)5);
writer.Write("action");
writer.Write("hash");
writer.Write((ushort)16123);
writer.Write((byte)3);
writer.Write(123213);

writer.Write((long)129387123);
writer.Write((float)0.9852f);
writer.Write(987987);
writer.Write((long)474747474747474747);
writer.Write(576657765);

byte[] written = stream.ToArray();

BanchoUserStats status = new();
status.ReadFromStream(new MemoryStream(written));

Bancho.InitializeBancho("127.0.0.1", 13382);
Bancho.Start();