using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bones : Collectable
{

	public UILabel bones_count;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnPugHit(HeroPug pug)
    {
		LevelController.current.addBones(1);
		bones_count = GameObject.Find ("bones_count").GetComponent<UILabel>();
		bones_count.text = LevelController.current.bones_count + "/8";
        this.CollectedHide();
    }
}
