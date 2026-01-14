import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import MainLayout from './layouts/MainLayout';
import LoginPage from './features/auth/LoginPage';
import ListaInmuebles from './features/inmuebles/ListaInmuebles';
import CrearInmueble from './features/inmuebles/CrearInmueble';
import DetalleInmueble from './features/inmuebles/DetalleInmueble';

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          {/* Ruta Publica */}
          <Route path="/login" element={<LoginPage />} />
          
          {/* Rutas Principales */}
          <Route element={<MainLayout />}>
            <Route path="/inmuebles" element={<ListaInmuebles />} />
            <Route path="/inmuebles/:id" element={<DetalleInmueble />} />
            <Route path="/crear-inmueble" element={<CrearInmueble />} />
          </Route>
          
          {/* Redireccion por defecto */}
          <Route path="*" element={<Navigate to="/login" />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;