using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterOwnerServer.Controllers
{
    [Route("api/owner")] 
    [ApiController] 
    public class OwnerController : ControllerBase 
    { 
        private ILoggerManager _logger; 
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) 
        { 
            _logger = logger; 
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet] 
        public async Task<IActionResult> GetAllOwners() 
        { 
            try 
            { 
                var owners = await _repository.Owner.GetAllOwnersAsync(); 
                _logger.LogInfo($"Tüm Tahsis Dataları Getirildi.");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"GetAllOwners action hata var: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "OwnerById")] 
        public async Task<IActionResult> GetOwnerById(Guid id) 
        { 
            try 
            { 
                var owner = await _repository.Owner.GetOwnerByIdAsync(id); 
                if (owner == null) 
                { 
                    _logger.LogError($"Tahsis id: {id}, database'de bulunamadı."); 
                    return NotFound(); 
                } 
                else 
                { 
                    _logger.LogInfo($"Tahsis bulundu: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"GetOwnerById action hata var: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}/account")] 
        public async Task<IActionResult> GetOwnerWithDetails(Guid id) 
        { 
            try 
            { 
                var owner = await _repository.Owner.GetOwnerWithDetailsAsync(id); 
                if (owner == null) 
                { 
                    _logger.LogError($"Tahsis detayları bulunamadı: {id}."); 
                    return NotFound(); 
                } 
                else 
                { 
                    _logger.LogInfo($"Tahsis detayları bulundu: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"GetOwnerWithDetails action hata var: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody]OwnerForCreationDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Tahsis boş gönderildi.");
                    return BadRequest("Tahsis boş");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Geçersiz tahsis gönderildi.");
                    return BadRequest("Geçersiz model");
                }

                var ownerEntity = _mapper.Map<Owner>(owner);

                _repository.Owner.CreateOwner(ownerEntity);
                await _repository.SaveAsync();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateOwner action hata var: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody]OwnerForUpdateDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Tahsis boş gönderildi.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Tahsis geçersiz gönderildi.");
                    return BadRequest("Geçersiz model");
                }

                var ownerEntity = await _repository.Owner.GetOwnerByIdAsync(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Tahsis id'si bulunamadı: {id}.");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);

                _repository.Owner.UpdateOwner(ownerEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateOwner action hata var: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            try
            {
                var owner = await _repository.Owner.GetOwnerByIdAsync(id);
                if (owner == null)
                {
                    _logger.LogError($"Tahsis id bulunamadı: {id}.");
                    return NotFound();
                }

                if (_repository.Meter.MetersByOwner(id).Any()) 
                {
                    _logger.LogError($"Tahsis silinemez: {id}. Tahsis'e ait sayaçların silinmesi gerekli."); 
                    return BadRequest("Tahsis'e ait sayaçların silinmesi gerekli"); 
                }

                _repository.Owner.DeleteOwner(owner);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOwner action hata var: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}