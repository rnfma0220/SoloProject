using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class BodyType : MonoBehaviour
{

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

    public static BodyEnum BodyTypeName(string name)
    {
        return name.ToLower() switch
        {
            "head" => BodyEnum.Head,
            "chest" => BodyEnum.Chest,
            "waist" => BodyEnum.Waist,
            "stomach" => BodyEnum.Stomach,
            "hips" => BodyEnum.Hips,
            "crotch" => BodyEnum.Crotch,
            "leftarm" => BodyEnum.LeftArm,
            "leftforarm" => BodyEnum.LeftForarm,
            "lefthand" => BodyEnum.LeftHand,
            "leftthigh" => BodyEnum.LeftThigh,
            "leftleg" => BodyEnum.LeftLeg,
            "leftfoot" => BodyEnum.LeftFoot,
            "rightarm" => BodyEnum.RightArm,
            "rightforarm" => BodyEnum.RightForarm,
            "righthand" => BodyEnum.RightHand,
            "rightthigh" => BodyEnum.RightThigh,
            "rightleg" => BodyEnum.RightLeg,
            "rightfoot" => BodyEnum.RightFoot,
            "ball" => BodyEnum.Ball,
            "spring" => BodyEnum.Spring,
            "meshhead" => BodyEnum.MeshHead,
            "meshnody" => BodyEnum.MeshBody,
            "agent" => BodyEnum.Agent,
            "cameratarget" => BodyEnum.CameraTarget,
            _ => BodyEnum.Root,
        };
    }

	public BodySet EnumToBodyPart(BodyEnum num)
	{
        return num switch
        {
            BodyEnum.Head => Head,
            BodyEnum.Chest => Chest,
            BodyEnum.Waist => Waist,
            BodyEnum.Stomach => Stomach,
            BodyEnum.Hips => Hips,
            BodyEnum.Crotch => Crotch,
            BodyEnum.LeftArm => LeftArm,
            BodyEnum.LeftForarm => LeftForarm,
            BodyEnum.LeftHand => LeftHand,
            BodyEnum.LeftThigh => LeftThigh,
            BodyEnum.LeftLeg => LeftLeg,
            BodyEnum.LeftFoot => LeftFoot,
            BodyEnum.RightArm => RightArm,
            BodyEnum.RightForarm => RightForarm,
            BodyEnum.RightHand => RightHand,
            BodyEnum.RightThigh => RightThigh,
            BodyEnum.RightLeg => RightLeg,
            BodyEnum.RightFoot => RightFoot,
            BodyEnum.Ball => Ball,
            BodyEnum.Spring => Spring,
            BodyEnum.MeshHead => MeshHead,
            BodyEnum.MeshBody => MeshBody,
            BodyEnum.Agent => Agent,
            BodyEnum.CameraTarget => CameraTarget,
            _ => Chest,
        };
    }
}
