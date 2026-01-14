import { useEffect, useState } from 'react';
import client from '../../api/axiosClient';
import { useAuth } from '../../context/AuthContext';
import Button from '../../components/Button';
import { X, Star, Save } from 'lucide-react';

export default function ModalClasificar({ foto, isOpen, onClose, onSave }) {
  const { strategy } = useAuth(); // Obtenemos la estrategia para saber permisos
  
  const [ambientes, setAmbientes] = useState([]);
  const [selectedAmbiente, setSelectedAmbiente] = useState('');
  const [esFavorita, setEsFavorita] = useState(false);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isOpen) {
      setEsFavorita(foto.esFavorita);
      setSelectedAmbiente(foto.ambienteId || ''); 

      // Cargamos ambientes si tiene permiso de clasificar
      if (strategy?.canClassify) {
        client.get('/Ambiente')
          .then(res => setAmbientes(res.data))
          .catch(err => console.error("Error cargando ambientes", err));
      }
    }
  }, [isOpen, foto, strategy]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!strategy?.canClassify) return;

    setLoading(true);
    try {
      const dto = {
        id: foto.id,
        esFavorita: esFavorita,
        ambienteId: selectedAmbiente ? parseInt(selectedAmbiente) : null
      };

      await client.put('/Foto/clasificar', dto);
      onSave(); 
      onClose();
    } catch (error) {
      alert("Error al guardar cambios");
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black/90 backdrop-blur-sm flex justify-center items-center z-50 p-4 transition-all">
      
      {/* Botón cerrar flotante */}
      <button 
        onClick={onClose}
        className="absolute top-4 right-4 text-white/70 hover:text-white transition-colors bg-white/10 p-2 rounded-full"
      >
        <X size={32} />
      </button>

      <div className="flex flex-col max-h-[90vh] w-full max-w-4xl bg-white rounded-xl overflow-hidden shadow-2xl">
        
        {/* --- IMAGEN GRANDE --- */}
        <div className="flex-1 bg-gray-100 flex items-center justify-center overflow-hidden relative group">
           <img 
               src={`https://localhost:7282/api/Foto/img/${foto.id}`} 
               className="max-h-[60vh] md:max-h-[70vh] w-full object-contain"
               alt="Vista previa"
           />
           {esFavorita && (
             <div className="absolute top-4 left-4 bg-yellow-400 text-white p-2 rounded-full shadow-lg z-10">
                <Star size={24} fill="white" strokeWidth={3} />
             </div>
           )}
        </div>

        {/* --- ZONA DE CONTROLES (Solo Grafista) --- */}
        {strategy?.canClassify ? (
            <form onSubmit={handleSubmit} className="p-6 bg-white border-t border-gray-100 flex flex-col md:flex-row gap-6 items-end md:items-center justify-between">
                
                <div className="flex-1 w-full md:w-auto flex flex-col md:flex-row gap-4">
                    {/* Selector */}
                    <div className="flex-1">
                        <label className="block text-xs font-bold text-gray-500 uppercase mb-1">Ambiente</label>
                        <select 
                            className="w-full border border-gray-300 p-2.5 rounded-lg text-gray-700 focus:ring-2 focus:ring-blue-500 outline-none font-medium"
                            value={selectedAmbiente}
                            onChange={(e) => setSelectedAmbiente(e.target.value)}
                        >
                            <option value="">-- Sin Clasificar --</option>
                            {ambientes.map(amb => (
                                <option key={amb.id} value={amb.id}>{amb.nombre}</option>
                            ))}
                        </select>
                    </div>

                    {/* Checkbox Favorito (Estilo botón) */}
                    <div>
                        <label className="block text-xs font-bold text-gray-500 uppercase mb-1">Destacado</label>
                        <label className={`cursor-pointer flex items-center justify-center gap-2 px-4 py-2.5 rounded-lg border transition-all select-none ${esFavorita ? 'bg-yellow-50 border-yellow-400 text-yellow-700 font-bold' : 'bg-white border-gray-300 text-gray-500 hover:bg-gray-50'}`}>
                            <input 
                                type="checkbox" 
                                className="hidden"
                                checked={esFavorita}
                                onChange={(e) => setEsFavorita(e.target.checked)}
                            />
                            <Star size={18} fill={esFavorita ? "currentColor" : "none"} />
                            {esFavorita ? 'Es Favorita' : 'Marcar Favorita'}
                        </label>
                    </div>
                </div>

                {/* Boton Guardar */}
                <Button type="submit" variant="primary" disabled={loading} className="w-full md:w-auto px-8">
                    {loading ? 'Guardando...' : <><Save size={18} /> Guardar Cambios</>}
                </Button>
            </form>
        ) : (
            // --- SOLO LECTURA (Admin / Fotografo) ---
            <div className="p-4 bg-white text-center border-t border-gray-100">
                <p className="text-gray-500 font-medium">{foto.nombreArchivo}</p>
                <div className="flex justify-center gap-2 mt-2">
                     <span className={`text-xs px-2 py-1 rounded font-bold ${foto.nombreAmbiente !== "Sin clasificar" ? 'bg-blue-100 text-blue-700' : 'bg-gray-100 text-gray-500'}`}>
                        {foto.nombreAmbiente}
                     </span>
                </div>
            </div>
        )}
      </div>
    </div>
  );
}