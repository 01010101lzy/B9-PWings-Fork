using System.Linq;

namespace WingProcedural
{
    public class WingTankResource : IConfigNode
    {
        public PartResourceDefinition resource;
        public float unitsPerVolume;
        public float ratio = 1.0f; // fuel type fraction = ratio/sum(ratio)

        public WingTankResource(ConfigNode node)
        {
            Load(node);
        }

        public void Load(ConfigNode node)
        {
            int resourceID = node.GetValue("name").GetHashCode();
            if (PartResourceLibrary.Instance.resourceDefinitions.Any(rd => rd.id == resourceID))
            {
                resource = PartResourceLibrary.Instance.resourceDefinitions[resourceID];
                float.TryParse(node.GetValue("ratio"), out ratio);
            }
        }

        public void Save(ConfigNode node) { }

        public void SetUnitsPerVolume(float ratioTotal)
        {
            if (resource.volume == 0)
                unitsPerVolume = ratio;
            else
                unitsPerVolume = ratio * 1000.0f / (resource.volume * ratioTotal);
        }
    }
}