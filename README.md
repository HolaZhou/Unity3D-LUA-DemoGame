## Unity3D-LUA-DemoGame

 本项目简单实现贪吃蛇的部分功能，场景中只有一个调用主lua的脚本，所有业务逻辑均基于Xlua由lua实现。
 
 做此demo目的在于熟悉lua语法与lua调用c#的基本用法，待项目用到时查看。
 

#部分总结：

*1、变量


--变量声明的时候 不需要声明类型

	str = "Hello World"
	
	--多个变量
	
	name,age ="ha",4
	
	--局部变量
	
	local i = 0
	
*2、数组


arr = {} --不限长度，默认从1开始

print(#arr) --获取数组长度

*3、创建物体


--c#:GameObject go = new GameObject(“test”);

local go = CS.UnityEngine.GameObject(“test”)

--c#:GameObject obj = Resources.Load("TetrisPanelOne");

--   GameObject gamePanel = Instantiate<GameObeject>(obj);

local obj = CS.UnityEngine.Resources.Load("TetrisPanelOne")

local gamePanel = CS.UnityEngine.Object.Instantiate(obj.gameObject)

*4、销毁物体


CS.UnityEngine.Object.Destroy(gameObject)

*5、查找物体


local text = self.transform:FindChild("UIRoot/Canvas/Text")

*6、获取组件


Local meshRenderer= self:GetComponent("MeshRenderer")

*7、调用方法


local r = CS.UnityEngine.Vector3.up*CS.UnityEngine.Time.deltaTime*speed

self.transform:Rotate(r)

*8、类型转换


tostring(data)

tonumber(data)
