﻿using System;
using System.Security;
using System.Security.Permissions;

namespace Samples
{
    public class DomainPermission
    {
        public static void SetDemo()
        {
            //创建文件 IO 读取权限
            FileIOPermission FileIOReadPermission = new FileIOPermission(PermissionState.None);
            FileIOReadPermission.AllLocalFiles = FileIOPermissionAccess.Read;

            //创建基本权限集
            PermissionSet BasePermissionSet = new PermissionSet(PermissionState.None); // PermissionState.Unrestricted 用于完全信任
            BasePermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            PermissionSet grantset = BasePermissionSet.Copy();
            grantset.AddPermission(FileIOReadPermission);

            //编写示例源文件以读取
            System.IO.File.WriteAllText("TEST.TXT", "File Content");

            //-------- 完全信任地调用方法 --------
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                ReadFileMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //-------- 创建具有文件 IO 读取权限的 AppDomain --------
            AppDomain sandbox = AppDomain.CreateDomain("Sandboxed AppDomain With FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset, null);
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                sandbox.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //-------- 创建没有文件 IO 读取权限的 AppDomain --------
            //应当引发安全异常
            PermissionSet grantset2 = BasePermissionSet.Copy();
            //grantset2.AddPermission(FileIOReadPermission);

            AppDomain sandbox2 = AppDomain.CreateDomain("Sandboxed AppDomain Without FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset2, null);
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                sandbox2.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("");
        }

        public static void ReadFileMethod()
        {
            string S = System.IO.File.ReadAllText("TEST.TXT");
            Console.WriteLine("File Content: " + S);
            Console.WriteLine("");
        }
    }
}