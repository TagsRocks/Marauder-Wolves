using UnityEngine;

namespace Scripts.Core.Tools.Render
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]

    public abstract class ProceduralMesh : MonoBehaviour
    {
        public Sprite Sprite;
        public Color Color = Color.white;
        public Material Material;
        public string SortingLayer = "Default";
        public int OrdetInLayer = 0;

        protected Mesh Mesh;

	    protected virtual void OnEnable()
	    {
            Mesh = new Mesh();
	        GetComponent<MeshFilter>().mesh = Mesh;

            Initialize();
		}

        protected virtual void Update()
        {
            if (Application.isPlaying)
            {
                CalculateVertices();
            }
            else
            {
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
            Mesh.Clear();

            ApplySorting();
            InitMaterial();

            CalculateVertices();
            CalculateTringles();
            CalculateNormals();
            CalculateUv();
        }

        private void ApplySorting()
        {
            GetComponent<Renderer>().sortingLayerName = SortingLayer;
            GetComponent<Renderer>().sortingOrder = OrdetInLayer;
        }

        protected virtual void InitMaterial()
        {
            if (Material == null)
            {
                var shader = Shader.Find("Sprites/Default");
                Material = new Material(shader);
            }

            GetComponent<Renderer>().sharedMaterial = Material;
            
            InitColor();
            InitTexture();
        }

        private void InitTexture()
        {
            if (Sprite == null)
                return;
         
            Sprite.texture.wrapMode = TextureWrapMode.Repeat;
            GetComponent<Renderer>().sharedMaterial.SetTexture("_MainTex", Sprite.texture);
        }

        private void InitColor()
        {
            GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color);
        }

        public abstract void SetVertexCount(int count);

        public abstract void SetPosition(int index, Vector3 position);

        protected abstract void CalculateVertices();

        protected abstract void CalculateNormals();
		
		protected abstract void CalculateUv();
		
		protected abstract void CalculateTringles();
    }
}
