/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
namespace Core.Unums
{
    public class BattleErrorMessage
    {
        uint EVENT_ERROR_FIRST_HOLE = 0x8000100B; /* Невозможно присоединиться к лидеру комнаты.\n Переход в Зал ожидания. */
	    uint EVENT_ERROR_FIRST_MAINLOAD = 0x8000100A; /* Присоединение к игре не состоялось. */
	    uint EVENT_ERROR_EVENT_BATTLE_TIMEOUT_CN = 0x80001006; /* Игра окончена в связи с сетевыми неполадками. */
	    uint EVENT_ERROR_EVENT_BATTLE_TIMEOUT_CS = 0x80001007;

	    private int _value;

	    BattleErrorMessage(int code) {
	    	_value = code;
	    }

	    public int get() {
	    	return _value;
	    }
    }
}