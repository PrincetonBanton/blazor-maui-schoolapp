using SchoolApp.Shared.Models;

namespace SchoolApp.Client.Helpers
{
    public static class TemplateValidator
    {
        public static (bool IsValid, string Message, string CssClass) Validate(List<Component> components)
        {
            double componentTotal = components.Sum(c => c.Percentage);

            if (Math.Abs(componentTotal - 100) > 0.01)
            {
                return (
                    false,
                    $"Not Validated: Total component percentage is {componentTotal:F2}%. It must be exactly 100%.",
                    "text-danger"
                );
            }

            foreach (var comp in components)
            {
                if (comp.Subcomponents == null || comp.Subcomponents.Count == 0)
                {
                    return (
                        false,
                        $"Not Validated: \"{comp.Name}\" has no subcomponents.",
                        "text-danger"
                    );
                }

                double subTotal = comp.Subcomponents.Sum(s => s.Percentage);
                if (Math.Abs(subTotal - 100) > 0.01)
                {
                    return (
                        false,
                        $"Not Validated: Subcomponents in \"{comp.Name}\" add up to {subTotal:F2}%. They must total 100%.",
                        "text-danger"
                    );
                }
            }

            return (true, string.Empty, string.Empty);
        }
    }
}
