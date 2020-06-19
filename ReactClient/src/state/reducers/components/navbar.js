
const initialState = {
  image: "/images/logo-curacao.png",
  logout: null,
  menuItems: [
    {
      id: 10,
      name: "Registration",
      feature: "",
      to: "/application-selector",
      menuItems: []
    },
    {
      id: 20,
      name: "Dashboards",
      feature: "",
      to: "/application-selector",
      menuItems: [
        {
          id: 21,
          name: "Map of Chronic Diseases",
          feature: "",
          to: "/application-selector",
          menuItems: []
        },
        {
          id: 22,
          name: "Cost of Healthcare",
          feature: "",
          to: "/application-selector",
          menuItems: []
        },
      ]
    },
    {
      id: 30,
      name: "Appointments",
      feature: "",
      to: "/application-selector",
      menuItems: []
    }
  ]
};

export default function componentNavbar(state = initialState, action) {
  switch (action.type) {
    default:
      return state;
  }
};
