using Core.Model;
using Game.Network.ServerPacket;
using Game.Network.ServerPacket.LOBBY;
using System;
using System.Linq;

namespace Game.Network.ClientPacket.LOBBY
{
    public class PROTOCOL_LOBBY_QUICKJOIN_REQ : ReceivePacket
    {
        public PROTOCOL_LOBBY_QUICKJOIN_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {

        }

        public override void RunImpl()
        {
            Channel channel = getClient().getPlayer().getChannel();

            int roomsCount = 0;
            int slotID = 0;

            foreach(var room in channel.getRooms().ToArray())
            {
                roomsCount = roomsCount + 1;
            }

            if(roomsCount == 0)
            {
                getClient().SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ACK());
            }
            else
            {
                if(roomsCount == 1)
                {
                    Room oneRoom = channel.getRoom(1 - 1);
                    int State = 0;

                    foreach(var slot in oneRoom.getRoomSlots().ToArray())
                    {
                        if(slot.getState() == SLOT_STATE.SLOT_STATE_EMPTY)
                        {
                            slotID = slot.getId();
                            getClient().getPlayer().setRoom(oneRoom);
                            oneRoom.addPlayer(getClient().getPlayer());

                            SLOT playerSlot = oneRoom.getRoomSlotByPlayer(getClient().getPlayer());
                            if (playerSlot == null)
                            {
                                getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(null, slotID, 0x80001004));
                                getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(oneRoom));
                                return;
                            }

                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                member.getClient().SendPacket(new PROTOCOL_ROOM_PLAYER_ENTER_ACK(playerSlot));
                            }

                            getClient().getPlayer().getChannel().removePlayer(getClient().getPlayer());
                            getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(oneRoom, playerSlot.getId(), 0));

                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(oneRoom));
                            }

                            BattleHandler.AddPlayer(getClient().getPlayer());

                            State = 1;
                        }
                    }

                    if(State == 0)
                    {
                        getClient().SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ACK());
                    }
                }
                else
                {
                    Room rndRoom = channel.getRoomInId(new Random().Next(1, roomsCount));
                    getClient().getPlayer().setRoom(rndRoom);
                    rndRoom.addPlayer(getClient().getPlayer());

                    SLOT playerSlot = rndRoom.getRoomSlotByPlayer(getClient().getPlayer());
                    if (playerSlot == null)
                    {
                        getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(null, slotID, 0x80001004));
                        getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(rndRoom));
                        return;
                    }

                    foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                    {
                        member.getClient().SendPacket(new PROTOCOL_ROOM_PLAYER_ENTER_ACK(playerSlot));
                    }

                    getClient().getPlayer().getChannel().removePlayer(getClient().getPlayer());
                    getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(rndRoom, playerSlot.getId(), 0));

                    foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                    {
                        member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(rndRoom));
                    }

                    BattleHandler.AddPlayer(getClient().getPlayer());
                }
            }
        }
    }
}
