// Decompiled with JetBrains decompiler
// Type: Game.FlyCamera
// Assembly: Survivalcraft, Version=2.2.10.4, Culture=neutral, PublicKeyToken=null
// MVID: FBA3446A-1D99-4668-AA17-5F426E81ECED
// Assembly location: d:\Users\12464\Desktop\sc2\DLLbao\dlls\Survivalcraft.dll

using Engine;
using Engine.Input;
using Game;

namespace CreatorModAPI
{
    /// <summary>
    /// 飞行相机
    /// 带有晃动效果，上下反向
    /// </summary>
    public class FPPRCamera : BasePerspectiveCamera
    {
        private Vector3 m_position;
        private Vector3 m_direction;
        private Vector3 m_velocity;
        private float m_rollSpeed;
        private float m_pitchSpeed;
        private float m_rollAngle;

        public FPPRCamera(GameWidget gameWidget)
          : base(gameWidget)
        {
        }
        //固定角色
        public override bool UsesMovementControls => false;

        public override bool IsEntityControlEnabled => true;
        public override void Activate(Camera previousCamera) => SetupPerspectiveCamera(previousCamera.ViewPosition, previousCamera.ViewDirection, previousCamera.ViewUp);

        public override void Update(float dt)
        {
            Vector3 vector3_1 = Vector3.Zero;
            Vector2 vector2 = Vector2.Zero;
            ComponentInput componentInput = GameWidget.PlayerData.ComponentPlayer?.ComponentInput;
            if (componentInput != null)
            {
                vector3_1 = componentInput.PlayerInput.CameraMove * new Vector3(1f, 0.0f, 1f);
                vector2 = componentInput.PlayerInput.CameraLook;
            }
            int num1 = Keyboard.IsKeyDown(Key.Shift) ? 1 : 0;
            bool flag = Keyboard.IsKeyDown(Key.Control);
            Vector3 direction = m_direction;
            Vector3 unitY = Vector3.UnitY;
            Vector3 axis = Vector3.Normalize(Vector3.Cross(direction, unitY));
            float num2 = 10f;
            if (num1 != 0)
            {
                num2 *= 5f;
            }

            if (flag)
            {
                num2 /= 5f;
            }

            Vector3 vector3_2 = Vector3.Zero + num2 * vector3_1.X * axis + num2 * vector3_1.Y * unitY + num2 * vector3_1.Z * direction;
            m_rollSpeed = MathUtils.Lerp(m_rollSpeed, -1.5f * vector2.X, 3f * dt);
            m_rollAngle += m_rollSpeed * dt;
            m_rollAngle *= MathUtils.Pow(0.33f, dt);
            m_pitchSpeed = MathUtils.Lerp(m_pitchSpeed, -0.2f * vector2.Y, 3f * dt);
            m_pitchSpeed *= MathUtils.Pow(0.33f, dt);
            m_velocity += 1.5f * (vector3_2 - m_velocity) * dt;
            _ = GameWidget.Target.ComponentBody.Position;
            // this.m_position= new Vector3(position.X, position.Y, position.Z);
            m_position += m_velocity * dt;
            m_direction = Vector3.Transform(m_direction, Matrix.CreateFromAxisAngle(unitY, 0.05f * m_rollAngle));
            m_direction = Vector3.Transform(m_direction, Matrix.CreateFromAxisAngle(axis, 0.2f * m_pitchSpeed));
            SetupPerspectiveCamera(m_position, m_direction, Vector3.TransformNormal(Vector3.UnitY, Matrix.CreateFromAxisAngle(m_direction, -m_rollAngle)));
        }
    }
}
