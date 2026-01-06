using L.GastosProdutos.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using L.GastosProdutos.Core.Application.Contracts.Group;
using L.GastosProdutos.Core.Application.Contracts.Group.V1.AddGroup;

namespace L.GastosProdutos.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all groups.
        /// </summary>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>List of groups.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a group by id.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>The group.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResponse>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="request">The group creation request.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>The created group id.</returns>
        [HttpPost]
        public async Task<ActionResult<AddGroupResponse>> Add(AddGroupRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing group.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <param name="request">The group update request.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, GroupWriteDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(id, request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes a group.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
