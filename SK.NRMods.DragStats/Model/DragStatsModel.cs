using BepInEx;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SK.NRMods.DragStats.Model
{
	internal class DragStatsModel
	{
		public float Speed { get; private set; }
		public float MaxSpeed { get; private set; }
		public RCC_CarControllerV3 RCC {  get; private set; }


		private readonly Dictionary<int, SpeedRecord> _speedRecords = [];
		private float _startTime = 0;

		private bool _isClean = true;

		private const float MoveSpeedThreshold = 0.2f;


		public void SetRccController(RCC_CarControllerV3 controller)
		{
			RCC = controller;
		}

		public void Update(float time, float speed)
		{
			if (speed < MoveSpeedThreshold && !_isClean)
			{
				Clear();
				return;
			}
			if (_isClean)
			{
				_startTime = time;
				_isClean = false;
			}
			Speed = speed;
			var roundedSpeed = (int)speed;
			if (roundedSpeed <= (int)MaxSpeed)
			{
				return;
			}
			var timeSinceStart = time - _startTime;
			MaxSpeed = speed;
			_speedRecords.Add(roundedSpeed, new SpeedRecord(timeSinceStart, roundedSpeed));
		}

		public float GetTimeBetweenSpeed(int from, int to)
		{
			if (from > to || from < 0 || to > MaxSpeed)
			{
				return 0;
			}
			return GetSpeedRecord(to).Time - GetSpeedRecord(from).Time;
		}

		public void Clear()
		{
			_speedRecords.Clear();
			_startTime = 0;
			MaxSpeed = 0;
			_isClean = true;
		}

		private SpeedRecord GetSpeedRecord(int speed)
		{
			if (speed == 0)
			{
				return new SpeedRecord();
			}
			if (_speedRecords.ContainsKey(speed))
			{
				return _speedRecords[speed];
			}

			//Debug.LogWarning($"no speed record: {speed}");
			var record = GetApproximateSpeedRecord(speed);
			_speedRecords.Add(speed, record);
			return record;
		}

		private SpeedRecord GetApproximateSpeedRecord(int speed)
		{
			var maxLowerSpeed = int.MaxValue;
			var minHigherSpeed = int.MinValue;

			if (_speedRecords.Count < 1)
			{
				return new SpeedRecord();
			}
			var keys = _speedRecords.Keys;
			foreach(var key in keys)
			{
				if (key < speed)
				{
					maxLowerSpeed = key;
				}
				else
				{
					minHigherSpeed = key;
					break;
				}
			}
			SpeedRecord lowerRecord;
			SpeedRecord higherRecord;
			if (maxLowerSpeed == int.MaxValue)
			{
				lowerRecord = new SpeedRecord();
			}
			else
			{
				lowerRecord = _speedRecords[maxLowerSpeed];
			}
			if (minHigherSpeed == int.MinValue)
			{
				return new SpeedRecord(); //TODO
			}
			higherRecord = _speedRecords[minHigherSpeed];
			var timeDiff = higherRecord.Time - lowerRecord.Time;
			var speedDiff = higherRecord.Speed - lowerRecord.Speed;
			var multiplier = (speed - lowerRecord.Speed) / speedDiff;
			return new SpeedRecord(lowerRecord.Time + timeDiff * multiplier, speed);
		}

		private readonly struct SpeedRecord(float time, int speed)
		{
			public readonly float Time = time;
			public readonly int Speed = speed;
		}
	}
}
