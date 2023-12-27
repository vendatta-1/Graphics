using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using GlmNet;
using System.Diagnostics;
namespace Graphics
{
    class Renderer
    {
        Shader sh;
		private int positionAttribLocation;
		private int colorAttribLocation;
        uint vertexBufferID;
        mat4 p;
        mat4 v;
        mat4 m;
        mat4 mvp;
        int MVP_ID;
     

        
        
        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(1, 1, 1f, 1);

			positionAttribLocation = Gl.glGetAttribLocation(sh.ProgramID, "position");
			colorAttribLocation = Gl.glGetAttribLocation(sh.ProgramID, "color");
            //Shape Coordinates 
		    float[] verts = { 
                // upper Head
				0.1f,0.7f,0.0f,
                0.71f,0.706f,0.668f,

                0.2f,0.6f,0.0f,
                0.71f,0.706f,0.668f,

                0.22f,0.55f,0.0f,
                0.71f,0.706f,0.668f,

                0.15f,0.6f,0.0f,
                0.71f,0.706f,0.668f,

                0.1f,0.55f,0.0f,
                0.71f,0.706f,0.668f,

                0.0f,0.6f,0.0f,
                0.71f,0.706f,0.668f,

                -0.1f,0.55f,0.0f,
                0.71f,0.706f,0.668f,

                -0.15f,0.6f,0.0f,
                0.71f,0.706f,0.668f,

                -0.22f,0.55f,0.0f,
                0.71f,0.706f,0.668f,

                -0.2f,0.6f,0.0f,
                0.71f,0.706f,0.668f,

                -0.1f,0.7f,0.0f,
                0.71f,0.706f,0.668f,


                // nose
                0.1f,0.55f,0.0f,
                0f,0f,0f,


                0.06f,0.47f,0.0f,
                0f,0f,0f,


                0.0f,0.5f,0.0f,
                0f,0f,0f,


                -0.06f,0.47f,0.0f,
                0f,0f,0f,

                -0.1f,0.55f,0.0f,
                0f,0f,0f,

                0.0f,0.6f,0.0f,
                0f,0f,0f,  


                // outre mouth
                0.1f,0.45f,0.0f,
                1,1,0,

                0.0f,0.5f,0.0f,
                1,1,0,

                -0.1f,0.45f,0.0f,
                1,1,0,

                0.0f,0.4f,0.0f,
                1,1,0,

                // inner mouth
                0.0f,0.47f,0.0f,
                0.71f,0.706f,0.668f,

                0.02f,0.45f,0.0f,
                0.71f,0.706f,0.668f,

                0.0f,0.43f,0.0f,
                0.71f,0.706f,0.668f,

                -0.02f,0.45f,0.0f,
                0.71f,0.706f,0.668f,

                // under tringle

                0.1f,0.35f,0.0f,
                0.71f,0.706f,0.668f,

                -0.1f,0.35f,0.0f,
                0.71f,0.706f,0.668f,

                0.0f,0.4f,0.0f,
                0.71f,0.706f,0.668f,


                // right face
                0.22f,0.55f,0.0f,
                0f,0f,0f,

                0.15f,0.6f,0.0f,
                0f,0f,0f,

                0.1f,0.55f,0.0f,
                0f,0f,0f,

                0.06f,0.47f,0.0f,
                0f,0f,0f,

                0.1f,0.45f,0.0f,
                0f,0f,0f,

                0.0f,0.4f,0.0f,
                0f,0f,0f,

                0.1f,0.35f,0.0f,
                0f,0f,0f,

                0.2f,0.4f,0.0f,
                0f,0f,0f,


                0.23f,0.5f,0.0f,
                0f,0f,0f,

                // left face 

                -0.22f,0.55f,0.0f,
                0f,0f,0f,

                -0.15f,0.6f,0.0f,
                0f,0f,0f,

                -0.1f,0.55f,0.0f,
                0f,0f,0f,

                -0.06f,0.47f,0.0f,
                0f,0f,0f,

                -0.1f,0.45f,0.0f,
                0f,0f,0f,

                -0.0f,0.4f,0.0f,
                0f,0f,0f,

                -0.1f,0.35f,0.0f,
                0f,0f,0f,

                -0.2f,0.4f,0.0f,
                0f,0f,0f,


                -0.23f,0.5f,0.0f,
                0f,0f,0f,

                // right eyes
                0.15f,0.53f,0.0f,
                1f,1f,1f,

                // left eyes
                -0.15f,0.53f,0.0f,
                1f,1f,1f,

                // left ear
                -0.2f,0.4f,0.0f,
                0.71f,0.706f,0.668f,

                -0.225f,0.5f,0.0f,
                0.71f,0.706f,0.668f,

                -0.25f,0.45f,0.0f,
                0.71f,0.706f,0.668f,

                -0.25f,0.35f,0.0f,
                0.71f,0.706f,0.668f,

                // right ear

                0.2f,0.4f,0.0f,
                0.71f,0.706f,0.668f,

                0.225f,0.5f,0.0f,
                0.71f,0.706f,0.668f,

                0.25f,0.45f,0.0f,
                0.71f,0.706f,0.668f,

                0.25f,0.35f,0.0f,
                0.71f,0.706f,0.668f,


                // left shoulder
                -0.1f,0.35f,0.0f,
                 0f,0f,0f,

                 -0.2f,0.4f,0.0f,
                 0f,0f,0f,

                 -0.25f,0.35f,0.0f,
                 0f,0f,0f,


                 -0.3f,0.25f,0.0f,
                 0f,0f,0f,

                 // right shoulder
                 0.1f,0.35f,0.0f,
                 0f,0f,0f,

                 0.2f,0.4f,0.0f,
                 0f,0f,0f,

                 0.25f,0.35f,0.0f,
                 0f,0f,0f,


                 0.3f,0.25f,0.0f,
                 0f,0f,0f,

                 //right arm 
                 0.1f,0.35f,0.0f,
                 0f,0f,0f,

                 0.3f,0.25f,0.0f,
                 0f,0f,0f,

                 0.55f,0.0f,0.0f,
                 0f,0f,0f,

                 0.50f,0.0f,0.0f,
                 0f,0f,0f,

                 0.25f,0.1f,0.0f,
                 0f,0f,0f,


                 //left arm 
                 -0.1f,0.35f,0.0f,
                 0f,0f,0f,

                 -0.3f,0.25f,0.0f,
                 0f,0f,0f,

                 -0.55f,0.0f,0.0f,
                 0f,0f,0f,

                 -0.50f,0.0f,0.0f,
                 0f,0f,0f,

                 -0.25f,0.1f,0.0f,
                 0f,0f,0f,

                 // uuper stomach
                0.1f,0.35f,0.0f,
                0.71f,0.706f,0.668f,

                -0.1f,0.35f,0.0f,
                0.71f,0.706f,0.668f,

                 -0.25f,0.1f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.25f,0.1f,0.0f,
                 0.71f,0.706f,0.668f,


                 // lower stomach
                 -0.25f,0.1f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.25f,0.1f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.35f,-0.2f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.0f,-0.3f,0.0f,
                 0.71f,0.706f,0.668f,

                 -0.35f,-0.2f,0.0f,
                 0.71f,0.706f,0.668f,


                 //lef hand
                 -0.5f,0.0f,0.0f,
                 0.71f,0.706f,0.668f,

                 -0.55f,0.0f,0.0f,
                 0.71f,0.706f,0.668f,

                 -0.60f,-0.15f,0.0f,
                 0.71f,0.706f,0.668f,

                 -0.53f,-0.13f,0.0f,
                 0.71f,0.706f,0.668f,


                 //right hand
                 0.5f,0.0f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.55f,0.0f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.60f,-0.15f,0.0f,
                 0.71f,0.706f,0.668f,

                 0.53f,-0.13f,0.0f,
                 0.71f,0.706f,0.668f,


                 // left i dont know 
                 -0.25f,0.1f,0.0f,
                 0,0,0,

                 -0.50f,0.0f,0.0f,
                 0,0,0,

                 -0.53f,-0.13f,0.0f,
                 0,0,0,

                 -0.43f,-0.1f,0.0f,
                 0,0,0,

                 -0.4f,0.0f,0.0f,
                 0,0,0,

                 -0.35f,-0.2f,0.0f,
                 0,0,0,



                 // right i dont know 
                 0.25f,0.1f,0.0f,
                 0,0,0,

                 0.50f,0.0f,0.0f,
                 0,0,0,

                 0.53f,-0.13f,0.0f,
                 0,0,0,

                 0.43f,-0.1f,0.0f,
                 0,0,0,

                 0.4f,0.0f,0.0f,
                 0,0,0,

                 0.35f,-0.2f,0.0f,
                 0,0,0,


                 // left i dont know 2
                 -0.4f,0.0f,0.0f,
                 0,0,0,

                 -0.35f,-0.2f,0.0f,
                 0,0,0,

                 -0.35f,-0.45f,0.0f,
                 0,0,0,

                 -0.45f,-0.25f,0.0f,
                 0,0,0,


                 // right i dont know 2
                 0.4f,0.0f,0.0f,
                 0,0,0,

                 0.35f,-0.2f,0.0f,
                 0,0,0,

                 0.35f,-0.45f,0.0f,
                 0,0,0,

                 0.45f,-0.25f,0.0f,
                 0,0,0,


                 // left i dont know 3

                 -0.35f,-0.2f,0.0f,
                 0f,0f,0f,

                 -0.35f,-0.45f,0.0f,
                 0f,0f,0f,

                 -0.15f,-0.55f,0.0f,
                 0f,0f,0f,

                 0.0f,-0.3f,0.0f,
                 0f,0f,0f,


                 // right i dont know 3

                 0.35f,-0.2f,0.0f,
                 0f,0f,0f,

                 0.35f,-0.45f,0.0f,
                 0f,0f,0f,

                 0.15f,-0.55f,0.0f,
                 0f,0f,0f,

                 0.0f,-0.3f,0.0f,
                 0f,0f,0f,


                 // left leg
                 -0.15f,-0.55f,0.0f,
                 1f,1f,0f,

                 -0.2f,-0.65f,0.0f,
                 1f,1f,0f,

                 -0.05f,-0.65f,0.0f,
                 1f,1f,0f,


                 // right leg
                 0.15f,-0.55f,0.0f,
                 1f,1f,0f,

                 0.2f,-0.65f,0.0f,
                 1f,1f,0f,

                 0.05f,-0.65f,0.0f,
                 1f,1f,0f,


                 // left i dont know 4
                 -0.35f,-0.45f,0.0f,
                 0f,0f,0f,

                 -0.2f,-0.65f,0.0f,
                 0f,0f,0f,

                 -0.15f,-0.55f,0.0f,
                 0f,0f,0f,

                 // right i dont know 4
                 0.35f,-0.45f,0.0f,
                 0f,0f,0f,

                 0.2f,-0.65f,0.0f,
                 0f,0f,0f,

                 0.15f,-0.55f,0.0f,
                 0f,0f,0f,

                 // last Draw
                 0.0f,-0.3f,0.0f,
                 0,0,0,

                 -0.15f,-0.55f,0.0f,
                 0,0,0,

                 -0.05f,-0.65f,0.0f,
                 0,0,0,

                 0.05f,-0.65f,0.0f,
                 0,0,0,

                 0.15f,-0.55f,0.0f,
                 0,0,0,

            };
          
            for (int i = 0; i < verts.Length; i += 6)
            {
                verts[i + 2] = 2.0f * verts[i + 2] - 1.0f;
            }
            vertexBufferID = GPU.GenerateBuffer(verts);
            //Projection Matrix
            p =  glm.perspective(45.0f, 3.0f / 3.0f, 1f, 100f);
            //View Matrix
            vec3 eye = new vec3(0, 2.3f, 1.5f);
            vec3 center = new vec3(0, 2, 0);
            vec3 up = new vec3(0, 1, 0);
            v = glm.lookAt(
                eye,
                center,
                up);
            //Model Matrix 
            m = new mat4(1);
            //mvp matrix
            List<mat4> matricesList = new List<mat4>();
            matricesList.Add(m);
            matricesList.Add(v);
            matricesList.Add(p);
            mvp=MathOperations.MultiplyMatrices(matricesList);
            sh.UseShader();
            ///Handle MVP uniform to control MVP var that exist in Shader using glGetUniformLocation(programId,name of existed uniform var in Vertexshader)
            MVP_ID = Gl.glGetUniformLocation(sh.ProgramID, "MVP");
            //pass values 
            Gl.glUniformMatrix4fv(MVP_ID, 1, Gl.GL_FALSE, mvp.to_array());
           

        }

        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            sh.UseShader();

            Gl.glEnableVertexAttribArray(positionAttribLocation);
            Gl.glEnableVertexAttribArray(colorAttribLocation);

            Gl.glVertexAttribPointer(positionAttribLocation, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), IntPtr.Zero);
            Gl.glVertexAttribPointer(colorAttribLocation, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            
			Gl.glDrawArrays(Gl.GL_POLYGON, 0, 11);
            Gl.glDrawArrays(Gl.GL_POLYGON, 11, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 17, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 21, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 25, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 28, 9);
            Gl.glDrawArrays(Gl.GL_POLYGON, 37, 9);

            Gl.glPointSize(5);
            Gl.glDrawArrays(Gl.GL_POINTS, 46, 2);
            Gl.glDrawArrays(Gl.GL_POLYGON, 48, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 52, 4);

            Gl.glDrawArrays(Gl.GL_POLYGON, 56, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 60, 4);

            Gl.glDrawArrays(Gl.GL_POLYGON, 64, 5);
            Gl.glDrawArrays(Gl.GL_POLYGON, 69, 5);


            Gl.glDrawArrays(Gl.GL_POLYGON, 74, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 78, 5);

            Gl.glDrawArrays(Gl.GL_POLYGON, 83, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 87, 4);


            Gl.glDrawArrays(Gl.GL_POLYGON, 91, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 97, 6);


            Gl.glDrawArrays(Gl.GL_POLYGON, 103, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 107, 4);


            Gl.glDrawArrays(Gl.GL_POLYGON, 111, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 115, 4);


            Gl.glDrawArrays(Gl.GL_POLYGON, 119, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 122, 3);


            Gl.glDrawArrays(Gl.GL_POLYGON, 125, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 128, 3);

            Gl.glDrawArrays(Gl.GL_POLYGON, 131, 5);



            Gl.glDisableVertexAttribArray(positionAttribLocation);
            Gl.glDisableVertexAttribArray(colorAttribLocation);
        }
        public void Update()
        {
            
        }
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
