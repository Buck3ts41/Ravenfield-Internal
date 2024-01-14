using UnityEngine;

namespace TestUnityInternal
{
    enum Tab
    {
        Aim,
        Visual,
        Misc
    }

    class Hacks : MonoBehaviour
    {
        //public Weapon.Configuration weaponConfiguration;
        //public Projectile.Configuration projectileConfiguraion;

        private bool ravenesp = false;
        private bool eagleesp = false;
        private bool raventraceline = false;
        private bool eagletraceline = false;
        private bool ravenhealthw = false;
        private bool eaglehealthw = false;
        private bool ravenstatus = false;
        private bool eaglestatus = false;
        private bool ravenweapone = false;
        private bool infmedi = false;
        private bool vehicleesp = false;
        private bool drawfov = false;
        private float fovRadius = 100f;
        private float crossdimension = 50f;
        private float crossthick = 1f;
        private float smooth = 0f;
        private bool vehiclename = false;
        private bool vehiclehealth = false;
        private bool aimbot = false;
        private bool objectesp = false;
        private bool godmode = false;
        private bool crosshair = false;
        private bool nofall = false;
        private bool vehiclegodmod = false;
        private bool multispeed = false;
        private bool headesp = false;
        private float speedmulti = 1f;
        private bool testesp = false;
        private bool grenadeesp = false;
        //private bool instareload = false;
        private bool infammo = false;
        //private bool nospread = false;
        //private bool norecoil = false;

        public Transform target;

        
        private bool showMenu = false;
        private Rect menuRect = new Rect(10f, 10f, 300f, 280f);
        private Tab currentTab = Tab.Aim;

        public void Update()
        {
            //ModifyWeaponConfiguration();
            //ModifyProjectileConfiguration();

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                showMenu = !showMenu;
            }
            if (infmedi == true && duration != null)
            {
                duration.reducedLifetimePerResupply = 0f;
            }
         

            if (Input.GetMouseButtonDown(1)) 
            {
                if (target != null)
                {
                    Vector3 headPos = target.position; 
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(headPos);

                    if (screenPos.z > 0) 
                    {
                        AimAtPoint(new Vector2(screenPos.x, Screen.height - screenPos.y));
                    }
                }
            }
        }

        //void ModifyProjectileConfiguration()
        //{
            //if (projectileConfiguraion != null)
            //{
                //if (damagemulti == true && localplayer.aiControlled == false)
                //{
                    //projectileConfiguraion.damage = multidamage;
                //}
            //}
        //}
        //void ModifyWeaponConfiguration()
        //{
            //if (weaponConfiguration != null)
            //{
                //if (infammo && localplayer.aiControlled == false)
                //{
                    //weaponConfiguration.ammo = 999;
                //}
                //if (instareload && localplayer.aiControlled == false)
                //{
                    //weaponConfiguration.reloadTime = 0f;
                //}
                //if (nospread && localplayer.aiControlled == false)
                //{
                    //weaponConfiguration.spread = 0f;
                //}
                //if (norecoil && localplayer.aiControlled == false)
                //{
                    //weaponConfiguration.kickback = 0f;
                //}
            //}
        //}
        public void OnGUI()
        {
            GUI.contentColor = Color.white;

            if (showMenu)
            {
                menuRect = GUI.Window(0, menuRect, DrawMenu, "Buck3ts41");
            }

            if (drawfov)
            {
                Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Render.DrawCircle(center, fovRadius, Color.black, 32, 2f);
            }

            if (crosshair)
            {
                Render.DrawCenteredLines(crossdimension, Color.red, crossthick);
            }

            foreach (Vehicle vehicle in UnityEngine.Object.FindObjectsOfType(typeof(Vehicle)) as Vehicle[])
            {
                Vector3 pivotPos = vehicle.transform.position;
                Vector3 vehicleFootPos; vehicleFootPos.x = pivotPos.x; vehicleFootPos.z = pivotPos.z; vehicleFootPos.y = pivotPos.y;
                Vector3 vehicleHeadPos; vehicleHeadPos.x = pivotPos.x; vehicleHeadPos.z = pivotPos.z; vehicleHeadPos.y = pivotPos.y + 2f;

                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(vehicleFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(vehicleHeadPos);

                if (vehiclegodmod && w2s_footpos.z > 0f && vehicle.dead == false && vehicle.claimedByPlayer == true)
                {
                    vehicle.maxHealth = 9999f;
                    vehicle.health = 9999f;
                }

                if (vehicleesp == true && w2s_footpos.z > 0f && vehicle.dead == false)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, Color.yellow);
                }
                if (vehiclename == true && w2s_footpos.z > 0f && vehicle.dead == false)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 40f);
                    string name = vehicle.name.ToString();
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), name);
                }
                if (vehiclehealth == true && w2s_footpos.z > 0f && vehicle.dead == false)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 18f);
                    string healthesp = vehicle.health.ToString();
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), healthesp);
                }
            }

            foreach (Actor player in UnityEngine.Object.FindObjectsOfType(typeof(Actor)) as Actor[])
            {
                Vector3 pivotPos = player.transform.position;
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y;
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f;

                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(w2s_headpos);

                if (objectesp == true && w2s_footpos.z > 0f)
                {
                    foreach (Medipack medikit in UnityEngine.Object.FindObjectsOfType(typeof(Medipack)) as Medipack[])
                    {

                        Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y + 30f);
                        GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), "MediKit");
                    }

                    foreach (Ammobox ammocrate in UnityEngine.Object.FindObjectsOfType(typeof(Ammobox)) as Ammobox[])
                    {

                        Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y + 30f);
                        GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), "AmmoBox");
                    }

                }

                if (grenadeesp && w2s_footpos.z > 0f)
                    foreach (GrenadeProjectile grenade in UnityEngine.Object.FindObjectsOfType(typeof(GrenadeProjectile)) as GrenadeProjectile[])
                    {
                        Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y + 30f);
                        GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), "Grenade");
                    }

                if (headesp == true && player != null && player.aiControlled == true && player.team == 1 && w2s_footpos.z > 0f)
                {
                    Render.DrawCircle(w2s_headpos, 5f, Color.magenta, 16, 2f);
                }

                if (infammo && player !=null && player.aiControlled == false)
                {
                    player.activeWeapon.ammo = 999;
                }

                if (multispeed == true && player != null && player.aiControlled == false)
                {
                    player.speedMultiplier = speedmulti;
                }

                if (testesp == true && player != null && player.aiControlled == true && player.team == 1 && w2s_footpos.z > 0f)
                {
                    float height = w2s_headpos.y - w2s_footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    //Render.DrawBoxOutline(new Vector2(w2s_footpos.x - (width / 2), (float)Screen.height - w2s_footpos.y - height), width, Color.red, 1f);

                }

                if (godmode && player != null && player.aiControlled == false)
                {
                    player.maxHealth = 9999f;
                    player.health = 9999f;
                }
                if (nofall && player != null && player.aiControlled == false)
                {
                    player.maxBalance = 9999f;
                    player.balance = 9999f;
                }

                if (aimbot && Input.GetMouseButtonDown(1) && screenPos.z > 0f && !player.dead)
                {
                    if (screenPos.z > 0) 
                    {
                        AimAtPoint(new Vector2(screenPos.x, Screen.height - screenPos.y));
                    }
                }
                
                if (ravenesp == true && player.team == 1 && w2s_footpos.z > 0f && player.dead == false && player.aiControlled == true)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, Color.red);
                }

                if (eagleesp == true && player.team == 0 && w2s_footpos.z > 0f && player.dead == false && player.aiControlled == true)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, Color.green);
                }

                if (raventraceline == true && player.team == 1 && w2s_footpos.z > 0f && player.dead == false && player.aiControlled == true)
                {
                    DrawTraceline(w2s_footpos, w2s_headpos, Color.red);
                }

                if (eagletraceline == true && player.team == 0 && w2s_footpos.z > 0f && player.dead == false && player.aiControlled == true)
                {
                    DrawTraceline(w2s_footpos, w2s_headpos, Color.green);
                }

                if (ravenhealthw == true && w2s_headpos.z > 0f && !player.dead && player.aiControlled == true && player.team == 1)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 18f);
                    string health = player.health.ToString();
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), health);
                }

                if (eaglehealthw == true && w2s_headpos.z > 0f && !player.dead && player.aiControlled == true && player.team == 0)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 18f);
                    string health = player.health.ToString();
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), health);
                }

                if (ravenweapone == true && w2s_headpos.z > 0f && !player.dead)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y + 50f);
                    string weapon = player.activeWeapon.ToString();
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), weapon);
                }

                if (ravenstatus == true && w2s_headpos.z > 0f && !player.dead && player.aiControlled == true && player.team == 1)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 40f);
                    string statusp = "Moving";
                    if (player.immersedInWater)
                    {
                        statusp = "Swimming";
                    }
                    if (player.parachuteDeployed)
                    {
                        statusp = "Parachute";
                    }
                    if (player.seat)
                    {
                        statusp = "Seated";
                    }
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), statusp);
                }

                if (eaglestatus == true && w2s_headpos.z > 0f && !player.dead && player.aiControlled == true && player.team == 0)
                {
                    Vector2 labelPos = new Vector2(w2s_headpos.x, Screen.height - w2s_headpos.y - 40f);
                    string statusp = "Moving";
                    if (player.immersedInWater)
                    {
                        statusp = "Swimming";
                    }
                    if (player.parachuteDeployed)
                    {
                        statusp = "Parachute";
                    }
                    if (player.seat)
                    {
                        statusp = "Seated";
                    }
                    GUI.Label(new Rect(labelPos.x, labelPos.y, 100f, 70f), statusp);
                }
            }
        }

        

        public void Start()
        {
            duration = FindObjectOfType<Medipack>();
            cameramov = FindObjectOfType<CameraSway>();
            damage = FindObjectOfType<Projectile>();
            reload = FindObjectOfType<Weapon>();
            localplayer = FindObjectOfType<Actor>();
            

        }

        public void DrawBoxESP(Vector3 footpos, Vector3 headpos, Color color)
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            Render.DrawBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 1f);
        }

        public void DrawTraceline(Vector3 footpos, Vector3 headpos, Color color)
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
        }

        void AimAtPoint(Vector2 targetScreenPos)
        {
            Vector2 centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 direction = (targetScreenPos - centerScreen).normalized;
            
            transform.Rotate(direction.y * smooth, -direction.x * smooth, 0f);
        }

        Medipack duration;
        CameraSway cameramov;
        Projectile damage;
        Weapon reload;
        Actor localplayer;
        
        void DrawMenu(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Aim"))
            {
                currentTab = Tab.Aim;
            }
            if (GUILayout.Button("Visual"))
            {
                currentTab = Tab.Visual;
            }
            if (GUILayout.Button("Misc"))
            {
                currentTab = Tab.Misc;
            }
            GUILayout.EndHorizontal();

            switch (currentTab)
            {
                case Tab.Aim:
                    aimbot = GUILayout.Toggle(aimbot, " Aimbot");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(" AimBot Smothness");
                    smooth = GUILayout.HorizontalSlider(smooth, 0f, 10f);
                    GUILayout.EndHorizontal();
                    drawfov = GUILayout.Toggle(drawfov, " Draw Fov Circle");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(" FOV Radius");
                    fovRadius = GUILayout.HorizontalSlider(fovRadius, 10f, 350f);
                    GUILayout.EndHorizontal();
               
                    break;

                case Tab.Visual:
                    GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical();
                    ravenesp = GUILayout.Toggle(ravenesp, " Raven Esp");
                    raventraceline = GUILayout.Toggle(raventraceline, " Raven Traceline");
                    ravenhealthw = GUILayout.Toggle(ravenhealthw, " Raven Health");
                    ravenstatus = GUILayout.Toggle(ravenstatus, " Raven Status");
                    ravenweapone = GUILayout.Toggle(ravenweapone, " Raven Weapon");
                    eagleesp = GUILayout.Toggle(eagleesp, " Eagle Esp");
                    eagletraceline = GUILayout.Toggle(eagletraceline, " Eagle Traceline");
                    eaglehealthw = GUILayout.Toggle(eaglehealthw, " Eagle Health");
                    eaglestatus = GUILayout.Toggle(eaglestatus, " Eagle Status");
                 
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical();
                    vehicleesp = GUILayout.Toggle(vehicleesp, " Vehicle Esp");
                    vehiclename = GUILayout.Toggle(vehiclename, " Vehicle Name");
                    vehiclehealth = GUILayout.Toggle(vehiclehealth, " Vehicle Health");
                    objectesp = GUILayout.Toggle(objectesp, " Object Esp");
                    grenadeesp = GUILayout.Toggle(grenadeesp, " Grenade Esp");
                    crosshair = GUILayout.Toggle(crosshair, " Crosshair");
                    GUILayout.Label(" Crosshair Dimension");
                    crossdimension = GUILayout.HorizontalSlider(crossdimension, 1f, 100f);
                    GUILayout.Label(" Crosshair Thickness");
                    crossthick = GUILayout.HorizontalSlider(crossthick, 0.5f, 3f);
                    
                    //headesp = GUILayout.Toggle(headesp, " Head Esp");
                    GUILayout.EndVertical();

                    GUILayout.EndHorizontal();
                    break;

                case Tab.Misc:
                    infmedi = GUILayout.Toggle(infmedi, " Infinite Medikit Duration");
                    godmode = GUILayout.Toggle(godmode, " Godmode");
                    nofall = GUILayout.Toggle(nofall, " No Ragdoll");
                    vehiclegodmod = GUILayout.Toggle(vehiclegodmod, " Vehicle Godmode");
                    infammo = GUILayout.Toggle(infammo, " Infinite Ammo");
                    multispeed = GUILayout.Toggle(multispeed, " Speed Multiplier");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(" Speed Multiplier");
                    speedmulti = GUILayout.HorizontalSlider(speedmulti, 1f, 15f);
                    GUILayout.EndHorizontal();
                    
                    
                    //instareload = GUILayout.Toggle(instareload, " Instant Reload");
                    //nospread = GUILayout.Toggle(nospread, " No Spread");
                    //norecoil = GUILayout.Toggle(norecoil, " No Recoil");
                    break;
                    
            }

            GUILayout.EndVertical();

           
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}
