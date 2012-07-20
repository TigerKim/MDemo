using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Resource;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace WebUI
{
    //public class WinsorControlerFactory : DefaultControllerFactory
    //{
    //    private WindsorContainer container;
    //    // 생성자 : 
    //    // 1. 새로운 IoC 컨테이너를 설정한다.
    //    // 2. Web.config에 지정된 모든 구성 요소를 등록한다.
    //    // 3. 모든 컨트롤러 형식을 구성요소로 등록한다.
    //    public WinsorControlerFactory()
    //    {
    //        // 컨테이너의 인스턴스를 생성하고 web.config로 부터 구성설정을 가져온다.
    //        container = new WindsorContainer(
    //            new XmlInterpreter(new ConfigResource()));
            
    //        // 그리고, 모든 컨트롤러 형식들을 Transient로 등록한다.
    //        var controllerTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
    //                              where typeof (IController).IsAssignableFrom(t) 
    //                              select t;

    //        foreach (Type t in controllerTypes)
    //        {
    //            //container.AddComponentWithLifestyle(t.FullName, t, LifeStyleType.Transient);
    //            //container.AddComponentLifeStyle(t.FullName, t, LifestyleType.Transient);
    //            //container.Register(Component.For<Type>().LifeStyle.Transient);
                
    //        }
    //    }

    //    // 요청시마다 서비스가 필요한 컨트롤러의 인스턴스를 생성한다.
    //    //protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
    //    //{
    //    //    return base.GetControllerInstance(requestContext, controllerType);
    //    //}
    //    protected override  IController GetControllerInstance(Type controllerType)
    //    {
    //        return (IController)container.Resolve(controllerType);
    //    }

    //}
}