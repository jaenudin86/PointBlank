using Core.Model;
using Core.Database.Tables;
using System.Collections.Generic;

namespace PointBlank.Network.ServerPacket
{
    public class PROTOCOL_AUTH_FRIEND_INFO_ACK : SendPacket
    {
        Account account;
        Player player;

        public PROTOCOL_AUTH_FRIEND_INFO_ACK(Account account)
        {
            this.account = account;
        }

        public static List<Friend> getFriends(ulong AccountID)
        {
            List<Friend> friends = new List<Friend>();

            foreach (Friend friend in FriendsTable.friends[AccountID].ToArray())
            {
                friends.Add(friend);
            }

            return friends;
        }

        public override void WriteImpl()
        {
            int status = 0;

            WriteH((short)274);
            WriteC((byte)getFriends(account.AccountID).Count);

            for (int i = 0; i < getFriends(account.AccountID).Count; i++)
            {
                player = PlayersTable.players[getFriends(account.AccountID)[i].FriendID];

                WriteC((byte)(player.getName().Length + 1));
                WriteS(player.getName(), player.getName().Length + 1);
                WriteD(1);

                if(player.getOnline() == true)
                {
                    if (player.getRoom() != null)
                    {
                        WriteD(player.getChannel().getId());
                        WriteH((short)player.getRoom().getId());
                        WriteC((byte)6);

                        status = 80;// in room
                    }
                    else
                    {
                        WriteD(0);
                        WriteH((short)0);
                        WriteC((byte)6);

                        status = 64;// online, not room
                    }
                }
                else
                {
                    WriteD(0);
                    WriteH((short)0);
                    WriteC((byte)6);

                    status = 48;// offline
                }

                if(getFriends(account.AccountID)[i].Status == 1)
                {
                    status = 32;
                }

                WriteC((byte) status);// status
                WriteH((short)player.getRank());
                WriteH((short)0);
            }
        }
    }
}
