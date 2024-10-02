using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Character;

public class BodyType : MonoBehaviour
{
    [HideInInspector] public Material headMaterial;
    [HideInInspector] public Material bodyMaterial;
    [HideInInspector] public string HexColor;
    public BodySet Head;
	public BodySet Chest;
	public BodySet Waist;
	public BodySet Stomach;
	public BodySet Hips;
	public BodySet Crotch;
	public BodySet LeftArm;
	public BodySet LeftForarm;
	public BodySet LeftHand;
	public BodySet LeftThigh;
	public BodySet LeftLeg;
	public BodySet LeftFoot;
	public BodySet RightArm;
	public BodySet RightForarm;
	public BodySet RightHand;
	public BodySet RightThigh;
	public BodySet RightLeg;
	public BodySet RightFoot;
	public BodySet Ball;
	public BodySet Spring;
	public BodySet MeshHead;
	public BodySet MeshBody;
	public BodySet Agent;
	public BodySet CameraTarget;
	public BodySet Root;

    #region 기절시 메스갑 저장하기위한 값
    public float HeadMass { get; private set; }
    public float ChestMass { get; private set; }
    public float WaistMass { get; private set; }
    public float StomachMass { get; private set; }
    public float HipsMass { get; private set; }
    public float CrotchMass { get; private set; }
    public float LeftArmMass { get; private set; }
    public float LeftForarmMass { get; private set; }
    public float LeftHandMass { get; private set; }
    public float LeftThighMass { get; private set; }
    public float LeftLegMass { get; private set; }
    public float LeftFootMass { get; private set; }
    public float RightArmMass { get; private set; }
    public float RightForarmMass { get; private set; }
    public float RightHandMass { get; private set; }
    public float RightThighMass { get; private set; }
    public float RightLegMass { get; private set; }
    public float RightFootMass { get; private set; }
    public float BallMass { get; private set; }
    public float SpringMass { get; private set; }
    #endregion

    private void OnEnable()
    {
        HexColor = UserManager.Instance.user.User_Color;
        SetUp();
    }

    public void SetUp()
    {
        SetupParts();
        SetupRenderers();
        SetupRigidbodys();
        SetupColliders();
        SetupColliderCheck();
    }

    private void SetupParts()
    {
        Transform parent = base.transform.Find("colliders");
        Root = new BodySet(base.transform, parent);
        Head = new BodySet("actor_head_collider", parent);
        Chest = new BodySet("actor_chest_collider", parent);
        Waist = new BodySet("actor_waist_collider", parent);
        Stomach = new BodySet("actor_stomach_collider", parent);
        Hips = new BodySet("actor_hips_collider", parent);
        Crotch = new BodySet("actor_crotch_collider", parent);
        LeftArm = new BodySet("actor_leftArm_collider", parent);
        LeftForarm = new BodySet("actor_leftForarm_collider", parent);
        LeftHand = new BodySet("actor_leftHand_collider", parent);
        LeftThigh = new BodySet("actor_leftThigh_collider", parent);
        LeftLeg = new BodySet("actor_leftLeg_collider", parent);
        LeftFoot = new BodySet("actor_leftFoot_helper", parent);
        RightArm = new BodySet("actor_rightArm_collider", parent);
        RightForarm = new BodySet("actor_rightForarm_collider", parent);
        RightHand = new BodySet("actor_rightHand_collider", parent);
        RightThigh = new BodySet("actor_rightThigh_collider", parent);
        RightLeg = new BodySet("actor_rightLeg_collider", parent);
        RightFoot = new BodySet("actor_rightFoot_helper", parent);
        Ball = new BodySet("actor_ball_collider", parent);
        Spring = new BodySet("actor_spring_helper", parent);
        MeshHead = new BodySet("actor_head_skinnedMesh", parent);
        MeshBody = new BodySet("actor_body_skinnedMesh", parent);
        Agent = new BodySet("helper_agent", parent);
        CameraTarget = new BodySet("helper_cameraTarget", parent);
    }

    private  void SetupRigidbodys()
    {
        Ball.PartRigidbody = Ball.PartTransform.GetComponent<Rigidbody>();
        BallMass = Ball.PartRigidbody.mass;
        Spring.PartRigidbody = Spring.PartTransform.GetComponent<Rigidbody>();
        SpringMass = Spring.PartRigidbody.mass;
        Hips.PartRigidbody = Hips.PartTransform.GetComponent<Rigidbody>();
        HipsMass = Hips.PartRigidbody.mass;
        Crotch.PartRigidbody = Crotch.PartTransform.GetComponent<Rigidbody>();
        CrotchMass = Crotch.PartRigidbody.mass;
        Waist.PartRigidbody = Waist.PartTransform.GetComponent<Rigidbody>();
        WaistMass = Waist.PartRigidbody.mass;
        Stomach.PartRigidbody = Stomach.PartTransform.GetComponent<Rigidbody>();
        StomachMass = Stomach.PartRigidbody.mass;
        Chest.PartRigidbody = Chest.PartTransform.GetComponent<Rigidbody>();
        ChestMass = Chest.PartRigidbody.mass;
        Head.PartRigidbody = Head.PartTransform.GetComponent<Rigidbody>();
        HeadMass = Head.PartRigidbody.mass;
        LeftArm.PartRigidbody = LeftArm.PartTransform.GetComponent<Rigidbody>();
        LeftArmMass = LeftArm.PartRigidbody.mass;
        LeftForarm.PartRigidbody = LeftForarm.PartTransform.GetComponent<Rigidbody>();
        LeftForarmMass = LeftForarm.PartRigidbody.mass;
        LeftHand.PartRigidbody = LeftHand.PartTransform.GetComponent<Rigidbody>();
        LeftHandMass = LeftHand.PartRigidbody.mass;
        LeftThigh.PartRigidbody = LeftThigh.PartTransform.GetComponent<Rigidbody>();
        LeftThighMass = LeftThigh.PartRigidbody.mass;
        LeftLeg.PartRigidbody = LeftLeg.PartTransform.GetComponent<Rigidbody>();
        LeftLegMass = LeftLeg.PartRigidbody.mass;
        LeftFoot.PartRigidbody = LeftFoot.PartTransform.GetComponent<Rigidbody>();
        LeftFootMass = LeftFoot.PartRigidbody.mass;
        RightArm.PartRigidbody = RightArm.PartTransform.GetComponent<Rigidbody>();
        RightArmMass = RightArm.PartRigidbody.mass;
        RightForarm.PartRigidbody = RightForarm.PartTransform.GetComponent<Rigidbody>();
        RightForarmMass = RightForarm.PartRigidbody.mass;
        RightHand.PartRigidbody = RightHand.PartTransform.GetComponent<Rigidbody>();
        RightHandMass = RightHand.PartRigidbody.mass;
        RightThigh.PartRigidbody = RightThigh.PartTransform.GetComponent<Rigidbody>();
        RightThighMass = RightThigh.PartRigidbody.mass;
        RightLeg.PartRigidbody = RightLeg.PartTransform.GetComponent<Rigidbody>();
        RightLegMass = RightLeg.PartRigidbody.mass;
        RightFoot.PartRigidbody = RightFoot.PartTransform.GetComponent<Rigidbody>();
        RightFootMass = RightFoot.PartRigidbody.mass;
        RigidbodySettings(Ball.PartRigidbody);
        RigidbodySettings(Spring.PartRigidbody);
        RigidbodySettings(Hips.PartRigidbody);
        RigidbodySettings(Crotch.PartRigidbody);
        RigidbodySettings(Waist.PartRigidbody);
        RigidbodySettings(Stomach.PartRigidbody);
        RigidbodySettings(Chest.PartRigidbody);
        RigidbodySettings(Head.PartRigidbody);
        RigidbodySettings(LeftArm.PartRigidbody);
        RigidbodySettings(LeftForarm.PartRigidbody);
        RigidbodySettings(LeftHand.PartRigidbody);
        RigidbodySettings(RightArm.PartRigidbody);
        RigidbodySettings(RightForarm.PartRigidbody);
        RigidbodySettings(RightHand.PartRigidbody);
        RigidbodySettings(LeftThigh.PartRigidbody);
        RigidbodySettings(LeftLeg.PartRigidbody);
        RigidbodySettings(LeftFoot.PartRigidbody);
        RigidbodySettings(RightThigh.PartRigidbody);
        RigidbodySettings(RightLeg.PartRigidbody);
        RigidbodySettings(RightFoot.PartRigidbody);
    }

    private void RigidbodySettings(Rigidbody rigidbody, bool useGravity = true)
    {
        rigidbody.drag = 1.5f;
        rigidbody.angularDrag = 1f;
        rigidbody.maxDepenetrationVelocity = 0.01f;
        rigidbody.maxAngularVelocity = 20f;
        rigidbody.useGravity = useGravity;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    private void SetupRenderers()
    {
        headMaterial = MeshHead.PartRenderer.material;
        bodyMaterial = MeshBody.PartRenderer.material;
        headMaterial.color = HexToColor(HexColor);
        bodyMaterial.color = HexToColor(HexColor);
        MeshHead.PartRenderer.material = headMaterial;
        MeshBody.PartRenderer.material = bodyMaterial;
    }

    public void ColorChange()
    {
        headMaterial.color = HexToColor(HexColor);
        bodyMaterial.color = HexToColor(HexColor);
        MeshHead.PartRenderer.material = headMaterial;
        MeshBody.PartRenderer.material = bodyMaterial;
    }

    private Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255);
    }

    private void SetupColliderCheck()
    {     
        AddColliderCheck(LeftHand.PartCollider);
        AddColliderCheck(LeftLeg.PartCollider);
        AddColliderCheck(LeftFoot.PartCollider);
        AddColliderCheck(RightHand.PartCollider);
        AddColliderCheck(RightLeg.PartCollider);
        AddColliderCheck(RightFoot.PartCollider);
    }

    private void AddColliderCheck(Collider partCollider)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "RoomScene") return;

        if (partCollider != null)
        {
            partCollider.gameObject.AddComponent<BodyColliderCheck>();
        }
    }

    private void SetupColliders()
    {
        Physics.IgnoreCollision(Chest.PartCollider, Head.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, LeftArm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, LeftForarm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, RightArm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, RightForarm.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Waist.PartCollider);
        Physics.IgnoreCollision(Chest.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Waist.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Waist.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Stomach.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Chest.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Waist.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, LeftThigh.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, LeftLeg.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, RightThigh.PartCollider);
        Physics.IgnoreCollision(Hips.PartCollider, RightLeg.PartCollider);
        Physics.IgnoreCollision(LeftForarm.PartCollider, LeftArm.PartCollider);
        Physics.IgnoreCollision(LeftForarm.PartCollider, LeftHand.PartCollider);
        Physics.IgnoreCollision(LeftArm.PartCollider, LeftHand.PartCollider);
        Physics.IgnoreCollision(RightForarm.PartCollider, RightArm.PartCollider);
        Physics.IgnoreCollision(RightForarm.PartCollider, RightHand.PartCollider);
        Physics.IgnoreCollision(RightArm.PartCollider, RightHand.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(LeftThigh.PartCollider, LeftLeg.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, Stomach.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, Crotch.PartCollider);
        Physics.IgnoreCollision(RightThigh.PartCollider, RightLeg.PartCollider);
    }
    
    public static Transform FindTransformViaName(Transform trans, string transformName)
    {
        if (trans.name == transformName)
        {
            return trans;
        }
        Transform transform = null;
        foreach (Transform tran in trans)
        {
            if (tran.name == transformName)
            {
                return tran;
            }
            transform = FindTransformViaName(tran, transformName);
            if (transform != null)
            {
                break;
            }
        }
        return transform;
    }
}
