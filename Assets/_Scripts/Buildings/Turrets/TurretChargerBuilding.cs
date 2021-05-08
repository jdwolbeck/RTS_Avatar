using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class TurretChargerBuilding : TurretTwoPartBuilding
    {
        public GameObject ChargeObjects = null;
        public Material EmptyCharge = null;
        public Material FullCharge = null;
        protected List<Transform> ChargeTransforms = null;
        protected int ChargeCount { get; set; }
        protected float ChargeIncrement { get; set; }
        protected int ChargeFillCount { get; set; }
        private int lastFillCount = -1;
        
        public override void Awake()
        {
            base.Awake();
            if (ChargeObjects != null)
            {
                ChargeTransforms = new List<Transform>();
                foreach (Transform child in ChargeObjects.transform)
                {
                    if(child != null)
                        ChargeTransforms.Add(child);
                }
                ChargeCount = ChargeTransforms.Count;
                ChargeIncrement = AtkSpeed / ChargeCount;
            }
        }
        protected override void Update()
        {
            if (ChargeTransforms != null)
                CalculateChargeDisplay();

            base.Update();
        }

        protected void CalculateChargeDisplay()
        {
            ChargeFillCount = (int)Mathf.Floor(atkCooldown / ChargeIncrement);

            if (ChargeFillCount != lastFillCount)
            {
                for (int i = 0; i < ChargeCount; i++)
                {
                    if (i < ChargeFillCount)
                        ChargeTransforms[i].gameObject.GetComponent<SpriteRenderer>().material = EmptyCharge;
                    else
                        ChargeTransforms[i].gameObject.GetComponent<SpriteRenderer>().material = FullCharge;
                }

                lastFillCount = ChargeFillCount;
            }
        }
    }
}