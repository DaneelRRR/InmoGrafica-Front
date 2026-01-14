import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import client from '../../api/axiosClient';
import { useAuth } from '../../context/AuthContext';
import ModalClasificar from '../fotos/ModalClasificar';
import GaleriaFotos from '../fotos/GaleriaFotos';
import Button from '../../components/Button';
import { ArrowLeft, MapPin, UploadCloud, Camera, Palette } from 'lucide-react';

export default function DetalleInmueble() {
    const { id } = useParams();
    const { user, strategy } = useAuth();
    const navigate = useNavigate();

    const [inmueble, setInmueble] = useState(null);
    const [loading, setLoading] = useState(true);
    const [uploading, setUploading] = useState(false);
    
    const [modalOpen, setModalOpen] = useState(false);
    const [selectedFoto, setSelectedFoto] = useState(null);

    const fetchDetalle = async () => {
        try {
            const response = await client.get(`/Inmueble/${id}`);
            setInmueble(response.data);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => { fetchDetalle(); }, [id]);

    const handleUpload = async (e, isEdited) => {
        const files = Array.from(e.target.files);
        if (files.length === 0) return;
        setUploading(true);
        const endpoint = isEdited ? '/Foto/subir-editada' : '/Foto/subir-raw';

        try {
            await Promise.all(files.map(async (file) => {
                const formData = new FormData();
                formData.append('archivo', file);
                formData.append('inmuebleId', id);
                formData.append('usuarioId', user.id);
                return client.post(endpoint, formData, { headers: { 'Content-Type': 'multipart/form-data' }});
            }));
            alert(`${files.length} fotos subidas.`);
            fetchDetalle(); 
        } catch (error) {
            alert("Error al subir.");
        } finally {
            setUploading(false);
            e.target.value = null; 
        }
    };

    const handleOpenModal = (foto) => {
        setSelectedFoto(foto);
        setModalOpen(true);
    };

    if (loading) return <div className="h-screen flex items-center justify-center text-gray-400">Cargando...</div>;
    if (!inmueble) return <div>No encontrado</div>;

    const fotosRaw = inmueble.fotos.filter(f => !f.esEditada);
    const fotosEditadas = inmueble.fotos.filter(f => f.esEditada);

    return (
        <div className="max-w-full mx-auto">
            
            {/* --- HEADER --- */}
            <div className="bg-white border-b border-gray-200 px-8 py-6 mb-6">
                <button onClick={() => navigate('/inmuebles')} className="text-gray-500 hover:text-blue-600 mb-4 flex items-center gap-1 text-sm font-medium transition-colors">
                    <ArrowLeft size={16} /> Volver al listado
                </button>
                
                <div className="flex flex-col md:flex-row md:items-end justify-between gap-4">
                    <div>
                        <h1 className="text-3xl font-extrabold text-gray-900 tracking-tight mb-2">{inmueble.nombre}</h1>
                        <div className="flex items-center text-gray-500 gap-2 text-sm">
                            <MapPin size={16} className="text-blue-500" />
                            <span>{inmueble.direccion}</span>
                            <span className="text-gray-300">|</span>
                            <span className="font-mono bg-gray-100 px-2 py-0.5 rounded text-xs text-gray-600">ID: {inmueble.id}</span>
                        </div>
                    </div>

                    <div className="flex gap-3">
                        {strategy?.canUploadRaw && (
                            <label className={`cursor-pointer group relative overflow-hidden bg-blue-600 hover:bg-blue-700 text-white px-5 py-2.5 rounded-lg shadow-md transition-all flex items-center gap-2 font-bold ${uploading ? 'opacity-75 pointer-events-none' : ''}`}>
                                <Camera size={18} />
                                {uploading ? 'Cargando...' : 'Subir RAW'}
                                <input type="file" multiple className="hidden" onChange={(e) => handleUpload(e, false)} />
                            </label>
                        )}

                        {strategy?.canUploadEdit && (
                            <label className={`cursor-pointer group bg-purple-600 hover:bg-purple-700 text-white px-5 py-2.5 rounded-lg shadow-md transition-all flex items-center gap-2 font-bold ${uploading ? 'opacity-75 pointer-events-none' : ''}`}>
                                <Palette size={18} />
                                {uploading ? 'Cargando...' : 'Entregar Finales'}
                                <input type="file" multiple className="hidden" onChange={(e) => handleUpload(e, true)} />
                            </label>
                        )}
                    </div>
                </div>
            </div>

            {/* --- CONTENIDO PRINCIPAL --- */}
            <div className="px-8 pb-12">
                <div className="grid grid-cols-1 lg:grid-cols-12 gap-8 items-start">
                    
                    {/* COLUMNA IZQUIERDA */}
                    <div className="lg:col-span-2 space-y-4">
                        <div className="sticky top-24"> {/* Truco: Se queda fijo al hacer scroll */}
                            <h2 className="text-lg font-bold text-gray-800 mb-3 flex items-center gap-2">
                                <UploadCloud size={20} className="text-gray-400" />
                                Material Crudo
                            </h2>
                            <GaleriaFotos 
                                fotos={fotosRaw} 
                                tipo="RAW" 
                                inmuebleId={id} 
                            />
                        </div>
                    </div>

                    {/* COLUMNA DERECHA */}
                    <div className="lg:col-span-10">
                        <h2 className="text-lg font-bold text-gray-800 mb-3 flex items-center gap-2">
                            <Palette size={20} className="text-purple-500" />
                            Entregables Finales
                        </h2>
                        
                        <GaleriaFotos 
                            fotos={fotosEditadas} 
                            tipo="EDITADA" 
                            inmuebleId={id}
                            onManage={handleOpenModal} 
                        />
                    </div>

                </div>
            </div>

            {/* MODAL */}
            {selectedFoto && (
                <ModalClasificar
                    isOpen={modalOpen}
                    foto={selectedFoto}
                    onClose={() => setModalOpen(false)}
                    onSave={() => fetchDetalle()}
                />
            )}
        </div>
    );
}