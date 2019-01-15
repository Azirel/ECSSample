using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;

namespace TwoStickPureExample
{
	public class PlayerMoveSystem : ComponentSystem
	{
		public struct Data
		{
			public readonly int Length;
			public ComponentDataArray<Position> Position;
			public ComponentDataArray<Rotation> Heading;
			public ComponentDataArray<PlayerInput> Input;
		}

		[Inject] private Data _data;

		float3 mousePosShift = new float3(0, 0, 84.24314f);
		protected override void OnUpdate()
		{
			var settings = TwoStickBootstrap.Settings;

			float dt = Time.deltaTime;
			for (int index = 0; index < _data.Length; ++index)
			{
				var position = _data.Position[index].Value;
				var rotation = _data.Heading[index].Value;
				var playerInput = _data.Input[index];
				float3 screenToWorldPoint = settings.MainCamera.ScreenToWorldPoint(playerInput.MousePosition + mousePosShift);

				position += dt * playerInput.Move * settings.playerMoveSpeed;
				rotation = quaternion.LookRotationSafe(math.normalize(screenToWorldPoint - position), Vector3.up);

				settings.DebugTextField.text = playerInput.FireCooldown.ToString();
				if (playerInput.FireCooldown <= float.Epsilon && playerInput.FirePressed)
				{
					playerInput.FireCooldown = settings.playerFireCoolDown;

					PostUpdateCommands.CreateEntity(TwoStickBootstrap.ShotSpawnArchetype);
					PostUpdateCommands.SetComponent(new ShotSpawnData
					{
						Shot = new Shot
						{
							TimeToLive = settings.bulletTimeToLive,
							Energy = settings.playerShotEnergy,
						},
						Position = new Position { Value = position },
						Rotation = new Rotation { Value = rotation },
						Faction = Factions.kPlayer,
					});
				}

				_data.Position[index] = new Position { Value = position };
				_data.Heading[index] = new Rotation { Value = rotation };
				_data.Input[index] = playerInput;
			}
		}
	}
}
