using UnityEngine.Rendering.Universal;

namespace UnityEditor.Rendering.Universal
{
    [VolumeComponentEditor(typeof(Tonemapping))]
    sealed class TonemappingEditor : VolumeComponentEditor
    {
        SerializedDataParameter m_Mode;

        public override void OnEnable()
        {
            var o = new PropertyFetcher<Tonemapping>(serializedObject);

            m_Mode = Unpack(o.Find(x => x.mode));
        }

        public override void OnInspectorGUI()
        {
            PropertyField(m_Mode);

            // Display a warning if the user is trying to use a tonemap while rendering in LDR
            var asset = UniversalRenderPipeline.asset;
            if (asset != null && !asset.supportsHDR)
            {
                EditorGUILayout.HelpBox("Tonemapping should only be used when working in HDR.", MessageType.Warning);
                return;
            }
        }
    }
}
