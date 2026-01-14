import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import client from '../../api/axiosClient';
import { useAuth } from '../../context/AuthContext';
import Button from '../../components/Button';
import { MapPin, Image as ImageIcon, Search, Home, Plus } from 'lucide-react';

export default function ListaInmuebles() {
    const [inmuebles, setInmuebles] = useState([]);
    const [busqueda, setBusqueda] = useState('');
    const [loading, setLoading] = useState(true);

    const { strategy, user } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchInmuebles = async () => {
            try {
                const response = await client.get('/Inmueble');
                setInmuebles(response.data);
            } catch (error) {
                console.error("Error cargando inmuebles", error);
            } finally {
                setLoading(false);
            }
        };
        fetchInmuebles();
    }, []);

    // Logica de Filtrado
    const inmueblesFiltrados = inmuebles.filter(bien =>
        bien.nombre.toLowerCase().includes(busqueda.toLowerCase()) ||
        bien.direccion.toLowerCase().includes(busqueda.toLowerCase()) ||
        bien.id.toString().includes(busqueda)
    );

    if (loading) return <div className="flex justify-center items-center h-64 text-gray-500">Cargando portafolio...</div>;

    return (
        <div className="min-h-screen bg-gray-50/50">

            {/* --- HEADER DE LA SECCION --- */}
            <div className="mb-8">
                <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                    <div>
                        <h1 className="text-3xl font-extrabold text-gray-900 tracking-tight">Portafolio de Propiedades</h1>
                        <p className="text-gray-500 mt-1">Gestiona y visualiza el estado de tus bienes raíces.</p>
                    </div>

                    {/* Boton Crear (Strategy) */}
                    {strategy?.canCreateInmueble && (
                        <Button onClick={() => navigate('/crear-inmueble')} className="shadow-lg hover:shadow-xl transform hover:-translate-y-0.5 transition-all">
                            <Plus size={20} /> Nuevo Inmueble
                        </Button>
                    )}
                </div>

                {/* --- BUSCADOR --- */}
                <div className="mt-6 relative max-w-md">
                    <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                        <Search size={18} className="text-gray-400" />
                    </div>
                    <input
                        type="text"
                        placeholder="Buscar por nombre, dirección o ID..."
                        className="block w-full pl-10 pr-3 py-3 border border-gray-200 rounded-xl leading-5 bg-white text-gray-900 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent shadow-sm transition-all"
                        value={busqueda}
                        onChange={(e) => setBusqueda(e.target.value)}
                    />
                </div>
            </div>

            {/* --- GRID DE TARJETAS --- */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                {inmueblesFiltrados.map((bien) => (
                    <div
                        key={bien.id}
                        onClick={() => navigate(`/inmuebles/${bien.id}`)}
                        className="group bg-white rounded-2xl shadow-sm hover:shadow-xl border border-gray-100 overflow-hidden cursor-pointer transition-all duration-300 transform hover:-translate-y-1"
                    >
                        {/* 1. FOTO DE PORTADA */}
                        <div className="h-68 bg-gray-50 relative overflow-hidden flex items-center justify-center">
                            {bien.imagenPortadaId ? (
                                <img
                                    src={`https://localhost:7282/api/Foto/img/${bien.imagenPortadaId}`}
                                    alt={bien.nombre}
                                    className="w-full h-full object-contain p-2 transition-transform duration-500 group-hover:scale-105"
                                    onError={(e) => { e.target.style.display = 'none'; e.target.nextSibling.style.display = 'flex'; }}
                                />
                            ) : null}

                            <div className={`absolute inset-0 flex items-center justify-center bg-gray-100 text-gray-400 ${bien.imagenPortadaId ? 'hidden' : 'flex'}`}>
                                <Home size={40} strokeWidth={1.5} />
                            </div>
                        </div>

                        {/* 2. DATOS DEL BIEN */}
                        <div className="p-5">
                            <h3 className="text-lg font-bold text-gray-900 mb-1 truncate group-hover:text-blue-600 transition-colors">
                                {bien.nombre}
                            </h3>

                            <div className="flex items-start gap-2 text-gray-500 mb-4 text-sm">
                                <MapPin size={16} className="mt-0.5 flex-shrink-0" />
                                <span className="line-clamp-2">{bien.direccion}</span>
                            </div>

                            <div className="pt-4 border-t border-gray-100 flex justify-between items-center">
                                <div className="flex items-center gap-1.5 text-gray-600 bg-gray-50 px-2 py-1 rounded-md">
                                    <ImageIcon size={16} />
                                    <span className="text-xs font-medium">{bien.cantidadFotos} Fotos</span>
                                </div>

                                <span className="text-blue-600 text-sm font-semibold opacity-0 group-hover:opacity-100 transition-opacity flex items-center gap-1">
                                    Ver Detalles →
                                </span>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            {inmueblesFiltrados.length === 0 && (
                <div className="text-center py-20">
                    <div className="bg-gray-100 rounded-full w-16 h-16 flex items-center justify-center mx-auto mb-4">
                        <Search size={30} className="text-gray-400" />
                    </div>
                    <h3 className="text-lg font-medium text-gray-900">No se encontraron propiedades</h3>
                    <p className="text-gray-500">Intenta buscar con otro término.</p>
                </div>
            )}
        </div>
    );
}