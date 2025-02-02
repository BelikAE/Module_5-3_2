using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_5_3_2
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectdElement = uidoc.Selection.PickObjects(ObjectType.Element, new PipeFilter(), "Выберите элементы");
            double sumLENGTH = 0;
            foreach (var selectedElem in selectdElement)
            {
                Element element = doc.GetElement(selectedElem);
                sumLENGTH += UnitUtils.ConvertFromInternalUnits(element.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), UnitTypeId.Meters);

            }

            TaskDialog.Show("Длина труб", $"Длина труб: {Math.Round(sumLENGTH, 2).ToString()} м.");

            return Result.Succeeded;
        }

        public class PipeFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                //Если элемент в фильтре является стеной тогда возращает значение Истина
                return elem is Pipe;
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }
    }
}
