// Definimos las reglas para cada rol
const STRATEGIES = {
  Administrador: {
    homeRedirect: '/inmuebles',
    canCreateInmueble: true,
    canUploadRaw: false,
    canUploadEdit: false,
    canClassify: false,
    navItems: [
      { label: 'Crear Inmueble', path: '/crear-inmueble' },
      { label: 'Ver Inmuebles', path: '/inmuebles' }
    ]
  },
  Fotografo: {
    homeRedirect: '/inmuebles',
    canCreateInmueble: false,
    canUploadRaw: true,
    canUploadEdit: false,
    canClassify: false,
    navItems: [
      { label: 'Mis Asignaciones', path: '/inmuebles' }
    ]
  },
  Grafista: {
    homeRedirect: '/inmuebles',
    canCreateInmueble: false,
    canUploadRaw: false,
    canUploadEdit: true,
    canClassify: true,
    navItems: [
      { label: 'Inmuebles para Editar', path: '/inmuebles' }
    ]
  }
};

// Funcion helper para obtener la estrategia actual
export const getRoleStrategy = (roleName) => {
  return STRATEGIES[roleName] || STRATEGIES['Invitado'];
};