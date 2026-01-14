import { Outlet, useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import Button from '../components/Button';

export default function MainLayout() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <div className="min-h-screen bg-gray-50 font-sans">
            {/* --- NAVBAR SUPERIOR --- */}
            <nav className="bg-white border-b border-gray-200 px-6 py-3 flex justify-between items-center sticky top-0 z-40 shadow-sm">

                <div className="flex items-center gap-8">
                    <Link to="/inmuebles" className="text-2xl font-extrabold text-blue-600 tracking-tight">
                        Inmo<span className="text-gray-800">Gráfica</span>
                    </Link>
                </div>

                {/* Seccion de Usuario */}
                <div className="flex items-center gap-4">
                    <div className="text-right hidden sm:block">
                        <p className="text-sm font-bold text-gray-800">{user?.nombreCompleto}</p>
                        <p className="text-xs text-blue-500 font-semibold uppercase">{user?.rol}</p>
                    </div>

                    {/* Avatar simple con iniciales */}
                    <div className="h-10 w-10 bg-blue-100 text-blue-600 rounded-full flex items-center justify-center font-bold border-2 border-white shadow-sm">
                        {user?.nombreCompleto?.charAt(0) || 'U'}
                    </div>

                    <Button variant="secondary" onClick={handleLogout} className="text-sm px-3 ml-2">
                        Salir
                    </Button>
                </div>
            </nav>

            {/* --- CONTENIDO DE LAS PAGINAS --- */}
            <main className="w-full p-6">
                <Outlet />
            </main>
        </div>
    );
}