using Scripts.Core.Utils;
using UnityEngine;

namespace Scripts.Core.Tools.Render
{
    public class LineMesh : ProceduralMesh
    {
        [SerializeField]protected Vector3[] Positions;
        [SerializeField]protected float Width = 0.5f;

        protected Vector3[] Vertices;

        public override void SetVertexCount(int count)
        {
            Positions = new Vector3[count];
        }

        public override void SetPosition(int index, Vector3 position)
        {
            Positions[index] = position;
        }

        protected override void CalculateVertices()
        {
            if(Positions == null || Positions.Length < 2)
                return;

            Vertices = new Vector3[Positions.Length*2];

            for (var i = 0; i < Positions.Length; i++)
            {
                var n = Math2Utils.GetNormal(Positions, i);

                Vertices[i*2] = Positions[i];
                Vertices[i*2 + 1] = Positions[i] + n * Width;
            }

            Mesh.vertices = Vertices;
        }

        protected override void CalculateTringles()
        {
            if (Positions == null || Positions.Length < 2)
                return;

            Mesh.triangles = ShapeTopology.TringleStrip(Positions.Length * 2 - 2);
        }

        protected override void CalculateUv()
        {
            if (Vertices == null || Positions.Length < 2 || Sprite == null)
                return;

            var uv = new Vector2[Vertices.Length];

            var ratio = (float)Sprite.texture.height/Sprite.texture.width;
            var k = ratio / Width;

            var x = 0f;

            uv[0] = new Vector2(x, 0);
            uv[1] = new Vector2(x, 0.99f);

            for (var i = 1; i < Positions.Length; i++)
            {
                var d = (Positions[i] - Positions[i - 1]).magnitude;
                
                x += k * d;

                uv[i*2] = new Vector2(x, 0);
                uv[i*2 + 1] = new Vector2(x, 0.99f);
            }

            Mesh.uv = uv;
        }

        protected override void CalculateNormals()
        {
            if (Vertices == null || Positions.Length < 2)
                return;

            var normals = new Vector3[Positions.Length * 2]; 
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = Vector3.forward;
            }

            Mesh.normals = normals;
        }
    }
}
